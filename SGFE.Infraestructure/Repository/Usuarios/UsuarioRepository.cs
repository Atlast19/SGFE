using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Usuarios;

namespace SGFE.Percistence.Repository.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioRepository> _logger;
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration, ILogger<UsuarioRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateUsuarioAsync(Usuario entity)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_Crear");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_Crear", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@Password", entity.PasswordHash);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("Usuario creado exitosamente");
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo crear el usuario");
                            throw new ArgumentException("No se pudo crear el usuario");
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

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_ObtenerPorEmail");
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_ObtenerPorEmail", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var usuario =  new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                                };
                                return usuario;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró un usuario con el email proporcionado.");
                                throw new ArgumentException("No se encontró un usuario con el email proporcionado.");
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

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_ObtenerPorId", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var usuario =  new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                                };
                                return usuario;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró un usuario con el ID proporcionado.");
                                throw new ArgumentException("No se encontró un usuario con el ID proporcionado.");
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

        public async Task<Usuario> LoginAsync(string Email, string PasswordHash)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_Login");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_Login", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@PasswordHash", PasswordHash);

                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var usuario = new Usuario
                                {
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                                };
                                return usuario;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró un usuario con el email y contraseña proporcionados.");
                                throw new ArgumentException("No se encontró un usuario con el email y contraseña proporcionados.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al iniciar sesión.");
                throw;
            }
        }

        public async Task UpdateUsuarioAsync(Usuario entity)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Usuario_Actualizar");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_Usuario_Actualizar", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", entity.Id);
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@Password", entity.PasswordHash);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("Usuario actualizado exitosamente");
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo actualizar el usuario");
                            throw new ArgumentException("No se pudo actualizar el usuario");
                        }
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