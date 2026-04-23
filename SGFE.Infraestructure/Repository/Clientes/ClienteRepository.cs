using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;
using SGFE.Domein.Entitys;

namespace SGFE.Percistence.Repository.Clientes
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ClienteRepository> _logger;
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration, ILogger<ClienteRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateClienteAsync(Cliente entity)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Cliente_Crear");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Cliente_Crear", connection)) 
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpresaId", entity.EmpresaId);
                        cmd.Parameters.AddWithValue("@TipoDocumento", entity.TipoDocumento);
                        cmd.Parameters.AddWithValue("@Documento", entity.Documento);
                        cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        cmd.Parameters.AddWithValue("@NombreComercial", entity.NombreComercial);
                        cmd.Parameters.AddWithValue("@Direccion", entity.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", entity.Telefono);
                        cmd.Parameters.AddWithValue("@Email", entity.Email);
                        cmd.Parameters.AddWithValue("@Activo", (object)entity.Activo ?? DBNull.Value);

                         await connection.OpenAsync();

                        var rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0) { 

                            _logger.LogInformation("Cliente creado exitosamente con Documento: {Documento}", entity.Documento);

                            var clientes = new Cliente 
                            {
                                EmpresaId = entity.EmpresaId,
                                TipoDocumento = entity.TipoDocumento,
                                Documento = entity.Documento,
                                Nombre = entity.Nombre,
                                NombreComercial = entity.NombreComercial,
                                Direccion = entity.Direccion,
                                Telefono = entity.Telefono,
                                Email = entity.Email,
                                Activo = entity.Activo
                            };

                        }
                        else{
                            _logger.LogWarning("No se pudo crear el cliente con Documento: {Documento}", entity.Documento);
                            throw new ArgumentException("No se pudo crear el cliente con Documento: " + entity.Documento);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el cliente");
                throw;
            }
        }

        public Task DeleteClienteAsync(int Id)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del procedimiento almacenado sp_Cliente_Eliminar con Id: {ClienteId}", Id);
                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Cliente_Eliminar", connection)) 
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        connection.Open();
                        var rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("Cliente eliminado exitosamente con Id: {ClienteId}", Id);

                            var clientes = new Cliente
                            {
                                Id = Id
                            };
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo eliminar el cliente con Id: {ClienteId}", Id);
                            throw new ArgumentException("No se pudo eliminar el cliente con Id: " + Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cliente con Id: {ClienteId}", Id);
                throw;
            }
             return Task.CompletedTask;
        }

        public async Task<List<Cliente>> GetAllClienteAsync()
        {
            try 
            {
                _logger.LogInformation("Ejecucion del procedimiento almacenado sp_Cliente_Crear");

                using (SqlConnection conection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Cliente_Crear", conection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        await conection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            var clientes = new List<Cliente>();

                            while (await reader.ReadAsync()) 
                            {
                                var cliente = new Cliente
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId")),
                                    TipoDocumento = reader.GetString(reader.GetOrdinal("TipoDocumento")),
                                    Documento = reader.GetString(reader.GetOrdinal("Documento")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("Activo")),
                                    FechaCreacion = reader.IsDBNull(reader.GetOrdinal("FechaCreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                    FechaActualizacion = reader.IsDBNull(reader.GetOrdinal("FechaActualizacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaActualizacion"))
                                };
                                clientes.Add(cliente);
                            }

                            if (!clientes.Any()) {
                                _logger.LogWarning("No se encontraron clientes registrados");
                                throw new ArgumentException("No se encontraron clientes registrados");
                            }

                            _logger.LogInformation("Se encontraron {Count} clientes", clientes.Count);
                            return clientes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los clientes");
                throw;
            }
        }

        public async Task<Cliente> GetClienteByEmpresaIdAsync(int EmpresaId)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Cliente_ObtenerPorEmpresaId con EmpresaId: {EmpresaId}", EmpresaId);
                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Cliente_ObtenerPorEmpresaId", connection)) 
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpresaId", EmpresaId);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) 
                        {
                            if (reader.HasRows) 
                            {
                                Cliente cliente = new Cliente();
                                while (await reader.ReadAsync()) 
                                {
                                    cliente.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                    cliente.EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId"));
                                    cliente.TipoDocumento = reader.GetString(reader.GetOrdinal("TipoDocumento"));
                                    cliente.Documento = reader.GetString(reader.GetOrdinal("Documento"));
                                    cliente.Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                    cliente.NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial"));
                                    cliente.Direccion = reader.GetString(reader.GetOrdinal("Direccion"));
                                    cliente.Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
                                    cliente.Email = reader.GetString(reader.GetOrdinal("Email"));
                                    cliente.Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("Activo"));
                                    cliente.FechaCreacion = reader.IsDBNull(reader.GetOrdinal("FechaCreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaCreacion"));
                                    cliente.FechaActualizacion = reader.IsDBNull(reader.GetOrdinal("FechaActualizacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaActualizacion"));
                                }
                                _logger.LogInformation("Cliente encontrado con EmpresaId: {EmpresaId}", EmpresaId);
                                return cliente;
                            }
                            else 
                            {
                                _logger.LogWarning("No se encontró un cliente con EmpresaId: {EmpresaId}", EmpresaId);
                                throw new ArgumentException($"No se encontró un cliente con EmpresaId: {EmpresaId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente por EmpresaId: {EmpresaId}", EmpresaId);
                throw;
            }
        }

        public async Task<Cliente> GetClienteByIdAsync(int ClienteId)
        {
            try
            {
                _logger.LogInformation("Ejecucion del procedimiento almacenado sp_Cliente_ObtenerPorId");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Cliente_ObtenerPorId", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", ClienteId);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                Cliente cliente = new Cliente();

                                while (await reader.ReadAsync())
                                {
                                    cliente.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                    cliente.EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId"));
                                    cliente.TipoDocumento = reader.GetString(reader.GetOrdinal("TipoDocumento"));
                                    cliente.Documento = reader.GetString(reader.GetOrdinal("Documento"));
                                    cliente.Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                    cliente.NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial"));
                                    cliente.Direccion = reader.GetString(reader.GetOrdinal("Direccion"));
                                    cliente.Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
                                    cliente.Email = reader.GetString(reader.GetOrdinal("Email"));
                                    cliente.Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("Activo"));
                                    cliente.FechaCreacion = reader.IsDBNull(reader.GetOrdinal("FechaCreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaCreacion"));
                                    cliente.FechaActualizacion = reader.IsDBNull(reader.GetOrdinal("FechaActualizacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaActualizacion"));
                                }
                                _logger.LogInformation("Cliente encontrado con Id: {ClienteId}", ClienteId);
                                return cliente;
                            }
                            else 
                            {
                                _logger.LogWarning("No se encontró un cliente con Id: {ClienteId}", ClienteId);
                                throw new ArgumentException($"No se encontró un cliente con Id: {ClienteId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente por Id: {ClienteId}", ClienteId);
                throw;
            }
        }

        public async Task UpdateClienteAsync(Cliente entity)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del procedimiento almacenado sp_Cliente_Actualizar con Id: {ClienteId}", entity.Id);

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Cliente_Actualizar", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@EmpresaId", entity.EmpresaId);
                        cmd.Parameters.AddWithValue("@TipoDocumento", entity.TipoDocumento);
                        cmd.Parameters.AddWithValue("@Documento", entity.Documento);
                        cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        cmd.Parameters.AddWithValue("@NombreComercial", entity.NombreComercial);
                        cmd.Parameters.AddWithValue("@Direccion", entity.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", entity.Telefono);
                        cmd.Parameters.AddWithValue("@Email", entity.Email);
                        cmd.Parameters.AddWithValue("@Activo", entity.Activo);
                        cmd.Parameters.AddWithValue("@FechaActualizacion", entity.FechaActualizacion);

                        await connection.OpenAsync();

                        var rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            var clientes = new Cliente
                            {
                                Id = entity.Id,
                                EmpresaId = entity.EmpresaId,
                                TipoDocumento = entity.TipoDocumento,
                                Documento = entity.Documento,
                                Nombre = entity.Nombre,
                                NombreComercial = entity.NombreComercial,
                                Direccion = entity.Direccion,
                                Telefono = entity.Telefono,
                                Email = entity.Email,
                                Activo = entity.Activo,
                                FechaActualizacion = entity.FechaActualizacion
                            };
                        }
                        else {                             
                            _logger.LogWarning("No se pudo actualizar el cliente con Id: {ClienteId}", entity.Id);
                            throw new ArgumentException("No se pudo actualizar el cliente con Id: " + entity.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente con Id: {ClienteId}", entity.Id);
                throw;
            }   
        }
    }
}
