using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.TipoECF
{
    public class TiposECFRepository : ITiposECFRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TiposECFRepository> _logger;
        private readonly string _connectionString;

        public TiposECFRepository(IConfiguration configuration, ILogger<TiposECFRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public Task CrearTiposECFAsync(TiposECF entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TiposECF>> GetAllTiposECFAsync()
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_TiposECF_ObtenerTodos");

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_TiposECF_ObtenerTodos", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            var tiposECFList = new List<TiposECF>();
                            while (await reader.ReadAsync()) 
                            {
                                TiposECF tipoECF = new TiposECF
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };
                                tiposECFList.Add(tipoECF);
                            }
                            return tiposECFList;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los TiposECF");
                throw;
            }
        }
    }
}
