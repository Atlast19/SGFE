
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.Roles
{
    public class RolRepository : IRolRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RolRepository> _logger;
        private readonly string _connectionString;

        public RolRepository(IConfiguration configuration, ILogger<RolRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateRoleAsync(Rol entity)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Rol_Crear");

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Rol_Crear", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", entity.Descripcion);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("Rol creado exitosamente");
                            var  Roles = new Rol
                            {
                                Nombre = entity.Nombre,
                                Descripcion = entity.Descripcion
                            };
                        }
                        else {
                            _logger.LogWarning("No se pudo crear el rol");
                            throw new ArgumentException("No se pudo crear el rol");
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

        public async Task<List<Rol>> GetAllRoleAsync()
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Rol_ObtenerTodos");

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Rol_ObtenerTodos", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            var rolesList = new List<Rol>();
                            while (await reader.ReadAsync()) 
                            {
                                var rol = new Rol
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    FechaCreacion = reader.IsDBNull(reader.GetOrdinal("FechaCreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                    FechaActualizado = reader.IsDBNull(reader.GetOrdinal("FechaActualizado")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaActualizado"))
                                };
                                rolesList.Add(rol);
                            }
                            _logger.LogInformation("Roles obtenidos exitosamente");
                            return rolesList;
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

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Rol_ObtenerPorId", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", RoleId);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var rol = new Rol
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    FechaCreacion = reader.IsDBNull(reader.GetOrdinal("FechaCreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                    FechaActualizado = reader.IsDBNull(reader.GetOrdinal("FechaActualizado")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaActualizado"))
                                };
                                _logger.LogInformation("Rol obtenido exitosamente");
                                return rol;
                            }
                            else
                            {
                                _logger.LogWarning("No se encontró el rol con ID: {RoleId}", RoleId);
                                throw new ArgumentException($"No se encontró el rol con ID: {RoleId}");
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
    }
}
