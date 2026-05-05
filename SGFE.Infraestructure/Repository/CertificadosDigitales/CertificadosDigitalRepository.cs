using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.CertificadosDigitales;
using System.Data;

namespace SGFE.Percistence.Repository.CertificadosDigitales
{
    public class CertificadosDigitalRepository : ICertificadosDigitalRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CertificadosDigitalRepository> _logger;
        private readonly string _connectionString;

        public CertificadosDigitalRepository(IConfiguration configuration, ILogger<CertificadosDigitalRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task UploadCertificadoDigitalAsync(CertificadosDigital entity)
        {
            try
            {
                // 🔐 Encriptar password
                byte[] passwordEncriptado = EncriptarPassword(entity.PasswordEncriptada);

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_InsertarCertificadoDigital", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreArchivo", entity.NombreArchivo);
                    cmd.Parameters.Add("@ArchivoCertificado", SqlDbType.VarBinary).Value = entity.ArchivoCertificado;
                    cmd.Parameters.Add("@PasswordEncriptada", SqlDbType.VarBinary).Value = passwordEncriptado;
                    cmd.Parameters.AddWithValue("@FechaVencimiento", entity.FechaVencimiento);
                    cmd.Parameters.AddWithValue("@Activo", entity.Activo);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar certificado digital");
                throw;
            }
        }


        // Ejemplo simple de encriptación (puedes mejorar luego)
        private byte[] EncriptarPassword(string password)
        {
            return System.Text.Encoding.UTF8.GetBytes(password);
        }


    }
}
