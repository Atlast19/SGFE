using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.TiposECF;

namespace SGFE.Percistence.Repository.TiposECF
{
    public class TiposECFRepository : ITipoECFRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<TiposECFRepository> _logger;


        public TiposECFRepository(ISqlConnectionFactory connectionFactory, ILogger<TiposECFRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;

        }

        public async Task<TipoECF> CrearTiposECFAsync(TipoECF entity)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_TiposECF_Crear");
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_TipoECF_Crear", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Codigo", entity.Codigo);
                        command.Parameters.AddWithValue("@Descripcion", entity.Descripcion);

                        var rowAffected = await command.ExecuteNonQueryAsync();

                        if (rowAffected > 0) 
                        {
                            _logger.LogInformation("TipoECF creado exitosamente");

                            return new TipoECF
                            {
                                Codigo = entity.Codigo,
                                Descripcion = entity.Descripcion
                            };
                        }
                        else 
                        {
                            _logger.LogWarning("No se pudo crear el TipoECF");
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo TipoECF");
                throw;
            }
        }

        public async Task<TipoECF> DeleteTipoECFAsync(int id)
        {
            try 
            {
                _logger.LogInformation($"Ejecucion del proceso almacenado sp_TiposECF_Eliminarpara el Id {id}");
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_TiposECF_Eliminar", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Tipo(e-CF) desactivado exitosamente");

                            return new TipoECF
                            {
                                Id = id
                            };
                        }

                        _logger.LogWarning(
                            "No se pudo desactivar el Tipo(e-CF) con ID: {Id}",id);

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el Tipo(e-CF) con Id {id}");
                throw;
            }
        }

        public async Task<List<TipoECF>> GetAllTiposECFAsync()
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_TiposECF_ObtenerTodos");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_TipoECF_ObtenerTodos", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            var tiposECFList = new List<TipoECF>();
                            while (await reader.ReadAsync()) 
                            {
                                TipoECF tipoECF = new TipoECF
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
                                };
                                tiposECFList.Add(tipoECF);
                            }

                            if (!tiposECFList.Any())
                            {
                                _logger.LogWarning("No se encontraron TiposECF en la base de datos");
                                return null;
                            }
                            else 
                            {
                                _logger.LogInformation($"Se encontraron {tiposECFList.Count} TiposECF en la base de datos");
                                return tiposECFList;
                            }
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

        public async Task<TipoECF> GetTipoECFByIdAsync(int id)
        {
            try 
            {
                _logger.LogInformation($"Ejecucion del proceso almacenado sp_TipoECF_ObtenerPorId para el Id {id}");
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_TiposECF_ObtenerPorId", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        
                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            if (await reader.ReadAsync()) 
                            {
                                TipoECF tipoECF = new TipoECF
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
                                };
                                _logger.LogInformation($"TipoECF con Id {id} obtenido exitosamente");
                                return tipoECF;
                            }
                            else 
                            {
                                _logger.LogWarning($"No se encontró el TipoECF con Id {id}");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el TipoECF con Id {id}");
                throw;
            }
        }

        public async Task<TipoECF> UpdateTipoECFAsync(TipoECF entity)
        {
            try 
            {
                _logger.LogInformation($"Ejecucion del proceso almacenado sp_TiposECF_Actualizar para el Id {entity.Id}");
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_TiposECF_Actualizar", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", entity.Id);
                        command.Parameters.AddWithValue("@Codigo", entity.Codigo);
                        command.Parameters.AddWithValue("@Descripcion", entity.Descripcion);


                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Tipo(e-CF) actualizado exitosamente");

                            return entity;
                        }

                        _logger.LogWarning(
                            "No se pudo actualizar el Tipo(e-CF) con ID: {Id}",entity.Id);

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el TipoECF con Id {entity.Id}");
                throw;
            }
        }
    }
}
