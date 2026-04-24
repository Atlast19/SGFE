
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Empresas;

namespace SGFE.Percistence.Repository.Empresas
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmpresaRepository> _logger;
        private readonly string _connectionString;

        public EmpresaRepository(IConfiguration configuration, ILogger<EmpresaRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateEmpresaAsync(Empresa entiry)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_Crear");

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Empresa_Crear", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RNC", entiry.RNC);
                        command.Parameters.AddWithValue("@Nombre", entiry.Nombre);
                        command.Parameters.AddWithValue("@NombreComercial", entiry.NombreComercial);
                        command.Parameters.AddWithValue("@Direccion", entiry.Direccion);
                        command.Parameters.AddWithValue("@Telefono", entiry.Telefono);
                        command.Parameters.AddWithValue("@Email", entiry.Email);
                        command.Parameters.AddWithValue("@Ambiente", entiry.Ambiente);
                        command.Parameters.AddWithValue("@Activo", entiry.Activo);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("Empresa creada exitosamente con RNC: {RNC}", entiry.RNC);

                            var empresaCreada = new Empresa
                            {
                                RNC = entiry.RNC,
                                Nombre = entiry.Nombre,
                                NombreComercial = entiry.NombreComercial,
                                Direccion = entiry.Direccion,
                                Telefono = entiry.Telefono,
                                Email = entiry.Email,
                                Ambiente = entiry.Ambiente,
                                Activo = entiry.Activo
                            };
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo crear la empresa con RNC: {RNC}", entiry.RNC);
                            throw new ArgumentException("No se pudo crear la empresa con RNC: " + entiry.RNC);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la empresa");
                throw;
            }
        }

        public async Task<List<Empresa>> GetAllEmpresaAsync()
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_ObtenerTodos");
                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Empresa_ObtenerTodos", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            var empresas = new List<Empresa>();

                            while (await reader.ReadAsync()) 
                            {
                                Empresa empresa = new Empresa
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    RNC = reader.GetString(reader.GetOrdinal("RNC")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Ambiente = reader.GetString(reader.GetOrdinal("Ambiente")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };
                                empresas.Add(empresa);
                            }
                            if (!empresas.Any()) 
                            {
                                _logger.LogInformation("No se encontraron empresas en la base de datos");
                                throw new ArgumentException("No se encontraron empresas en la base de datos");
                            }

                            _logger.LogInformation("Se obtuvieron {Count} empresas de la base de datos", empresas.Count);
                            return empresas;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de empresas");
                throw;
            }
        }

        public async Task<Empresa> GetEmpresaByIdAsync(int empresaId)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_ObtenerPorId con ID: {EmpresaId}", empresaId);

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Empresa_ObtenerPorId", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", empresaId);

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            if (await reader.ReadAsync()) 
                            {
                                Empresa empresa = new Empresa
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    RNC = reader.GetString(reader.GetOrdinal("RNC")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Ambiente = reader.GetString(reader.GetOrdinal("Ambiente")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };
                                _logger.LogInformation("Empresa con ID: {EmpresaId} obtenida exitosamente", empresaId);
                                return empresa;
                            }
                            else 
                            {
                                _logger.LogWarning("No se encontró la empresa con ID: {EmpresaId}", empresaId);
                                throw new ArgumentException($"No se encontró la empresa con ID: {empresaId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la empresa con ID: {EmpresaId}", empresaId);
                throw;
            }
        }

        public async Task UpdateEmpresaAsync(Empresa entity)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_Actualizar con ID: {EmpresaId}", entity.Id);

                    using (SqlConnection connection = new SqlConnection(_connectionString)) 
                    {
                        using (SqlCommand command = new SqlCommand("sp_Empresa_Actualizar", connection)) 
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
    
                            command.Parameters.AddWithValue("@Id", entity.Id);
                            command.Parameters.AddWithValue("@RNC", entity.RNC);
                            command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                            command.Parameters.AddWithValue("@NombreComercial", entity.NombreComercial);
                            command.Parameters.AddWithValue("@Direccion", entity.Direccion);
                            command.Parameters.AddWithValue("@Telefono", entity.Telefono);
                            command.Parameters.AddWithValue("@Email", entity.Email);
                            command.Parameters.AddWithValue("@Ambiente", entity.Ambiente);
                            command.Parameters.AddWithValue("@Activo", entity.Activo);
    
                            await connection.OpenAsync();
                            var rowsAffected = await command.ExecuteNonQueryAsync();
    
                            if (rowsAffected > 0)
                            {
                                _logger.LogInformation("Empresa con ID: {EmpresaId} actualizada exitosamente", entity.Id);
                                
                            }
                            else
                            {
                                _logger.LogWarning("No se pudo actualizar la empresa con ID: {EmpresaId}", entity.Id);
                                throw new ArgumentException($"No se pudo actualizar la empresa con ID: {entity.Id}");
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la empresa con ID: {EmpresaId}", entity.Id);
                throw;
            }
        }
    }
}
