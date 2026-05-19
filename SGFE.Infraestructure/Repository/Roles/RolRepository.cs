using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Roles;

namespace SGFE.Percistence.Repository.Roles
{
    public class RolRepository : IRolRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<RolRepository> _logger;

        public RolRepository(ISqlConnectionFactory connectionFactory, ILogger<RolRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<Rol> CreateRoleAsync(Rol entity)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Rol_Crear");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_Role_Crear", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", entity.Descripcion);


                        var result = await command.ExecuteScalarAsync();

                        if (result != null && Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Rol creado exitosamente");

                            var role = new Rol
                            {
                                Nombre = entity.Nombre,
                                Descripcion = entity.Descripcion
                            };

                            return role;
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo crear el rol");
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el rol");
                throw;
            }
        }

        public async Task<Rol> DeleteRolAsync(int RoleId)
        {
            try 
            {
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Role_Desactivar", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", RoleId);

                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Rol desactivado exitosamente");

                            var rol = new Rol
                            {
                                Id = RoleId
                            };
                            return rol;
                        }

                        _logger.LogWarning(
                            "No se pudo desactivar el rol con ID: {RoleId}",RoleId);

                        return null;
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error al eliminar el rol");
                throw;
            }
        }

        public async Task<List<Rol>> GetAllRoleAsync()
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Rol_ObtenerTodos");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Role_ObtenerTodos", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            var rolesList = new List<Rol>();

                            while (await reader.ReadAsync())
                            {
                                var rol = new Rol
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
                                };
                                rolesList.Add(rol);
                            };

                            if (!rolesList.Any())
                            {
                                _logger.LogWarning("No se encontraron datos en la base de datos");
                                return null;
                            }
                            else 
                            {
                                _logger.LogInformation("Datos cargados correctamente");
                                return rolesList;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error al obtener los roles");
                throw;
            }
        }

        public async Task<Rol> GetRoleByIdAsync(int RoleId)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Rol_ObtenerPorId");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Role_ObtenerPorId", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", RoleId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var rol = new Rol
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
                                };
                                _logger.LogInformation("Rol obtenido exitosamente");
                                return rol;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró el rol con ID: {RoleId}", RoleId);
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol por ID");
                throw;
            }
        }

        public async Task<Rol> UpdateRolAsync(Rol entity)
        {
            try
            {
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Role_Actualizar", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", entity.Id);
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", entity.Descripcion);

                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Rol actualizado exitosamente");

                            return entity;
                        }

                        _logger.LogWarning(
                            "No se pudo actualizar el rol con ID: {RoleId}",
                            entity.Id);

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol");
                throw;
            }
        }
    }
}
