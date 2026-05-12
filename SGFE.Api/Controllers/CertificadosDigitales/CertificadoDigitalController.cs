using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.CertificadosDigitales;
using SGFE.Application.Models.CertificadosDigitales;
using System.Text;

namespace SGFE.Api.Controllers.CertificadosDigitales
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificadoDigitalController : ControllerBase
    {
        private readonly ICertificadoDigitalService _service;

        public CertificadoDigitalController(ICertificadoDigitalService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost("uploadCertificadoDigitalAsync")]
        public async Task<IActionResult> Upload(IFormFile archivo, [FromForm] string password,[FromForm] DateTime fechaVencimiento)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo inválido");

            // Carpeta destino
            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Certificados");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Nombre único
            var nombreArchivoUnico = $"{Guid.NewGuid()}_{archivo.FileName}";
            var rutaCompleta = Path.Combine(carpeta, nombreArchivoUnico);

            // Guardar archivo en disco
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Convertir a bytes (para BD si decides guardarlo)
            byte[] archivoBytes;
            using (var ms = new MemoryStream())
            {
                await archivo.CopyToAsync(ms);
                archivoBytes = ms.ToArray();
            }

            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var dto = new CreateCertificadoDigitalModel
            {
                NombreArchivo = archivo.FileName,
                RutaArchivo = rutaCompleta,
                ArchivoCertificado = archivoBytes,
                PasswordEncriptada = passwordBytes,
                FechaVencimiento = fechaVencimiento
            };

            await _service.UploadCertificadoDigitalAsync(dto);

            return Ok(new { mensaje = "Certificado cargado correctamente" });
        }
    }
}
