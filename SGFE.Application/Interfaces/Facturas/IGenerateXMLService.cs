

using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Facturas
{
    public interface IGenerateXMLService
    {
        string GenerarXml(Factura factura);
    }
}
