
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.SecuenciasNCF;
using System.Data;

namespace SGFE.Percistence.Repository.SecuenciasNCF
{
    public class SecuenciaNCFRepository : ISecuenciaNCFRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SecuenciaNCFRepository> _logger;
        private readonly string _connectionString;

        public SecuenciaNCFRepository(IConfiguration configuration, ILogger<SecuenciaNCFRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> GetNextSecuenciaNCFAsync(int empresaId, int tipoECFId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_SecuenciaNCF_ObtenerSiguiente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId);
                    cmd.Parameters.AddWithValue("@TipoECFId", tipoECFId);

                    // Parámetro de salida
                    SqlParameter outputNCF = new SqlParameter("@NCF", SqlDbType.NVarChar, 19)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputNCF);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return outputNCF.Value?.ToString();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error al obtener la secuencia NCF");
                throw;
            }
        }

        public async Task RegistrarSecuenciaAsync(SecuenciaNCF entity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_SecuenciaNCF_Registrar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpresaId", entity.EmpresaId);
                    cmd.Parameters.AddWithValue("@TipoECFId", entity.TipoECFId);
                    cmd.Parameters.AddWithValue("@Prefijo", entity.Prefijo);
                    cmd.Parameters.AddWithValue("@RangoInicio", entity.RangoInicio);
                    cmd.Parameters.AddWithValue("@RangoFin", entity.RangoFin);
                    cmd.Parameters.AddWithValue("@VigenciaDesde", entity.VigenciaDesde);
                    cmd.Parameters.AddWithValue("@VigenciaHasta", entity.VigenciaHasta);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar secuencia NCF");
                throw;
            }
        }
    }
}
