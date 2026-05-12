
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.SecuenciasNCF;
using System.Data;

namespace SGFE.Percistence.Repository.SecuenciasNCF
{
    public class SecuenciaNCFRepository : ISecuenciaNCFRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<SecuenciaNCFRepository> _logger;

        public SecuenciaNCFRepository(ISqlConnectionFactory connectionFactory, ILogger<SecuenciaNCFRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task RegistrarSecuenciaAsync(SecuenciaNCF entity)
        {
            try
            {
                using (SqlConnection conn = await _connectionFactory.CreateConnectionAsync())
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
