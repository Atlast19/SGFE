using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Usuarios;

namespace SGFE.Percistence.Repository.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ILogger<UsuarioRepository> _logger;
        private readonly ISqlConnectionFactory _connectionFactory;

        public UsuarioRepository(ILogger<UsuarioRepository> logger, ISqlConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario entity)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_Crear");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_Crear", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RolId", entity.RolId);
                        command.Parameters.AddWithValue("@EmpresaId", entity.EmpresaId);
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);


                        var result = await command.ExecuteScalarAsync();

                        if (result != null && Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Usuario creado exitosamente");

                            return new Usuario
                            {
                                Rol = entity.Rol,
                                EmpresaId = entity.EmpresaId,
                                Nombre = entity.Nombre,
                                Email = entity.Email,
                                PasswordHash = entity.PasswordHash
                            };
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo crear el usuario");

                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                throw;
            }
        }

        public async Task<Usuario> DeleteUsuarioAsync(int Id)
        {
            try 
            {
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_Desactivar", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", Id);

                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Usuario desactivado exitosamente");

                            return new Usuario
                            {
                                Id = Id
                            };  
                        }

                        _logger.LogWarning(
                            "No se pudo desactivar el usuario con ID: {RoleId}", Id);

                        return null;
                    }
                }
            }
            catch 
            {
                _logger.LogError($"No se puedieron eliminar los datos del ID: {Id}");
                throw;
            }
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            try 
            {
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuarios_ObtenerTodos", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        
                        using (var reader = await command.ExecuteReaderAsync()) 
                        {
                            var usuarios = new List<Usuario>();
                            
                            while (await reader.ReadAsync())
                            {
                                var usuario = new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    RolId = reader.GetInt32(reader.GetOrdinal("RolId")),
                                    EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Email = reader.GetString(reader.GetOrdinal("Email"))
                                };
                                usuarios.Add(usuario);
                            
                            };

                            if (!usuarios.Any())
                            {
                                _logger.LogWarning("No se encontraron datos en la base de datos");
                                return null;
                            }
                            else 
                            {
                                _logger.LogInformation("Datos cargados correctamente");
                                return usuarios;
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los usuarios.");
                throw;
            }
        }

        public async Task<Usuario> GetEmailForLogin(string email)
        {
            try 
            {
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_GetUserByEmail_Login", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var usuario = new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                                    EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId"))
                                };
                                return usuario;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró un usuario con el email proporcionado.");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el email para login.");
                throw;
            }
        }

        public async Task<List<string>> GetRolesByUsuarioIdAsync(int usuarioId)
        {
            try 
            {
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_ObtenerPorRolId_Login", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", usuarioId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var roles = new List<string>();
                            if (await reader.ReadAsync())
                            {
                                var role = reader.GetString(reader.GetOrdinal("Nombre"));
                                roles.Add(role);

                                return roles;
                            }
                            else 
                            {
                                _logger.LogWarning("No se encontraron roles en la base de datos");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los roles por ID de usuario.");
                throw;
            }
        }

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_ObtenerPorEmail");
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_ObtenerPorEmail", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);

                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var usuario =  new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    RolId = reader.GetInt32(reader.GetOrdinal("RolId")),
                                    EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Email = reader.GetString(reader.GetOrdinal("Email"))
                                };
                                return usuario;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró un usuario con el email proporcionado.");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario por email.");
                throw;
            }
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_ObtenerPorId");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_ObtenerPorId", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var usuario =  new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    RolId = reader.GetInt32(reader.GetOrdinal("RolId")),
                                    EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Email = reader.GetString(reader.GetOrdinal("Email"))
                                };
                                return usuario;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró un usuario con el ID proporcionado.");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario por ID.");
                throw;
            }
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario entity)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_Actualizar");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_Actualizar", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", entity.Id);
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);

                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Usuario actualizado exitosamente");

                            return entity;
                        }

                        _logger.LogWarning(
                            "No se pudo actualizar el usuario con ID: {UserId}", entity.Id);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario.");
                throw;
            }
        }
    }
}