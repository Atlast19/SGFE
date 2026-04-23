using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.CertificadosDigitales
{
    public class CertificadosDigitalesRepository : ICertificadosDigitalesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CertificadosDigitalesRepository> _logger;
        private readonly string _connectionString;

        public CertificadosDigitalesRepository(IConfiguration configuration, ILogger<CertificadosDigitalesRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public Task CrearCertificadoDigitalAsync(CertificadosDigital entoty)
        {
            throw new NotImplementedException();
        }

        public async Task<CertificadosDigital> GetEstadoCertificadoDigalAsync(int EmpresaID)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_CertificadosDigitales_ObtenerEstado para la empresa con ID {EmpresaID}", EmpresaID);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_CertificadosDigitales_ObtenerEstado", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpresaID", EmpresaID);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                CertificadosDigital certificado = new CertificadosDigital
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    //Activo = reader.GetString(reader.GetOrdinal("Estado")),
                                    //FechaVencimiento = reader.IsDBNull(reader.GetOrdinal("FechaExpiracion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaExpiracion"))
                                };
                                return certificado;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el estado del certificado digital para la empresa con ID {EmpresaID}", EmpresaID);
                throw;
            }
        }
    }
}
