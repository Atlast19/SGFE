using SGFE.Application.Interfaces.CertificadosDigitales;
using SGFE.Application.Interfaces.Facturas;
using SGFE.Application.Models.Facturas;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Facturas;
using System.Text;

namespace SGFE.Application.Services.Facturas
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _repository;
        private readonly ICertificadoDigitalService _certificadoRepository;
        private readonly IFirmaXmlService _firmaService;
        private readonly GenerateXMLFactory _xmlFactory;

        public FacturaService(IFacturaRepository repository,
            ICertificadoDigitalService certificadoRepository, 
            IFirmaXmlService firmaService,
            GenerateXMLFactory xmlFactory)
        {
            _repository = repository;
            _certificadoRepository = certificadoRepository;
            _firmaService = firmaService;
            _xmlFactory = xmlFactory;
        }

        public async Task<string> CreateFacturaAsync(CreateFacturaModel model)
        {
            var factura = new Factura
            {
                EmpresaId = model.EmpresaId,
                ClienteId = model.ClienteId,
                TipoECFId = model.TipoECFId,
                FechaEmision = DateTime.Now
            };

            var detalles = model.Detalles.Select(d => new FacturaDetalle
            {
                Descripcion = d.Descripcion,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,

                Monto = d.Cantidad * d.PrecioUnitario,
                Itbis = (d.Cantidad * d.PrecioUnitario) * 0.18m
            }).ToList();

            // Generar la factura en la base de datos
            var (facturaId, ncf) = await _repository.CreateFacturaAsync(factura, detalles);

            // Obtener la factura completa con los detalles para generar el XML
            var facturaCompleta = await _repository.GetfacturaByIdAsync(facturaId);

            // Obtiene el tipo de factura electronica
            var generator = _xmlFactory.Generator(facturaCompleta.TiposECF);

            // Generar el XML de la factura electronica espesifica
            var xml = generator.GenerarXml(facturaCompleta);

            // Obtener el certificado digital para firmar el XML
            var cert = await _certificadoRepository.GetCertificadoDigitalByIdAsync(model.CertificadoId);

            // Desencriptar la contraseña del certificado
            var password = DesencriptarPassword(cert.PasswordEncriptada);

            // Firmar el XML con el certificado digital
            var xmlFirmado = _firmaService.FirmarXml(xml,cert.RutaArchivo, password);

            // Actualizar el estado de la factura a "Firmada"
            await _repository.UpdateFacturaEstado(facturaId, "Firmada");

            // Guardar el XML firmado en la base de datos
            await _repository.GuardarXmlAsync(facturaId, xmlFirmado);


            return ncf;
        }

        public async Task<GetFacturaModel> GetfacturaByIdAsync(int FacturaId)
        {
            var factura = await _repository.GetfacturaByIdAsync(FacturaId);

            var facturas = new GetFacturaModel
            {
                Id = factura.Id,
                NCF = factura.NCF,
                TiposECF = factura.TiposECF,
                TipoECFId = factura.TipoECFId,
                FechaEmision = factura.FechaEmision,
                MontoTotal = factura.MontoTotal,
                ItbisTotal = factura.ItbisTotal,
                SubTotal = factura.SubTotal,
                ClienteNombre = factura.ClienteNombre,
                ClienteDocumento = factura.ClienteDocumento,
                Detalles = factura.FacturaDetalles.Select(d => new GetFacturaDetalleModel
                {
                    Descripcion = d.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Monto = d.Monto,
                    Itbis = d.Itbis
                }).ToList()
            };

            return facturas;
        }

        public async Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio)
        {
            await _repository.UpdateDGIIResponse(facturaId, trackId, estado, respuestaDGII, fechaEnvio);
        }

        public async Task UpdateFacturaEstado(int facturaId, string estado)
        {
            await _repository.GetfacturaByIdAsync(facturaId);
        }

        private string DesencriptarPassword(byte[] passwordEncriptada)
        {
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
                aes.IV = Encoding.UTF8.GetBytes("1234567890123456");

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] passwordDesencriptada = decryptor.TransformFinalBlock(
                        passwordEncriptada,
                        0,
                        passwordEncriptada.Length
                    );

                    return Encoding.UTF8.GetString(passwordDesencriptada);
                }
            }
        }
    }

}
