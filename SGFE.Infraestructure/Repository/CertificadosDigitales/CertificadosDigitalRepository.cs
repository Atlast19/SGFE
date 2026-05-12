using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.CertificadosDigitales;
using System.Data;
using System.Text;

namespace SGFE.Percistence.Repository.CertificadosDigitales
{
    public class CertificadosDigitalRepository : ICertificadosDigitalRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<CertificadosDigitalRepository> _logger;

        public CertificadosDigitalRepository(ISqlConnectionFactory connectionFactory, ILogger<CertificadosDigitalRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<CertificadosDigital> GetCertificadoDigitalByIdAsync(int id)
        {
            try 
            {
                using var conn = await _connectionFactory.CreateConnectionAsync();
                using var cmd = new SqlCommand("sp_CertificadosDigitales_ObtenerPorId", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);


                using var reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                    return null;

                await reader.ReadAsync();

                return new CertificadosDigital
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    NombreArchivo = reader["NombreArchivo"].ToString(),
                    RutaArchivo = reader["RutaArchivo"].ToString(),
                    ArchivoCertificado = (byte[])reader["ArchivoCertificado"],
                    PasswordEncriptada = (byte[])reader["PasswordEncriptada"],
                    FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]),
                    Activo = Convert.ToBoolean(reader["Activo"])
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener certificado digital con ID {id}");
                throw;
            }
        }

        public async Task UploadCertificadoDigitalAsync(CertificadosDigital entity)
        {
            try
            {
                // Encriptar password
                byte[] passwordEncriptado = EncriptarPassword(entity.PasswordEncriptada);

                using (SqlConnection conn = await _connectionFactory.CreateConnectionAsync())
                using (SqlCommand cmd = new SqlCommand("sp_Certificado_Crear", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreArchivo", entity.NombreArchivo);
                    cmd.Parameters.AddWithValue("@RutaArchivo", entity.RutaArchivo);
                    cmd.Parameters.Add("@ArchivoCertificado", SqlDbType.VarBinary).Value = entity.ArchivoCertificado;
                    cmd.Parameters.Add("@PasswordEncriptada", SqlDbType.VarBinary).Value = passwordEncriptado;
                    cmd.Parameters.AddWithValue("@FechaVencimiento", entity.FechaVencimiento);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar certificado digital");
                throw;
            }
        }

        private byte[] EncriptarPassword(byte[] password)
        {
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); // 32 bytes
                aes.IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return encryptor.TransformFinalBlock(password, 0, password.Length);
                }
            }
        }


    }
}
