using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Empresas;

namespace SGFE.Percistence.Repository.Empresas
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ILogger<EmpresaRepository> _logger;


        public EmpresaRepository(ISqlConnectionFactory sqlConnectionFactory, ILogger<EmpresaRepository> logger)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _logger = logger;

        }

        public async Task<Empresa> CreateEmpresaAsync(Empresa entity)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_Crear");

                using (SqlConnection connection = await _sqlConnectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Empresa_Crear", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RNC", entity.RNC);
                        command.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        command.Parameters.AddWithValue("@NombreComercial", entity.NombreComercial);
                        command.Parameters.AddWithValue("@Direccion", entity.Direccion);
                        command.Parameters.AddWithValue("@Telefono", entity.Telefono);
                        command.Parameters.AddWithValue("@Email", entity.Email);


                        var result = await command.ExecuteScalarAsync();

                        if (result != null && Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation(
                                "Empresa creada exitosamente con RNC: {RNC}",
                                entity.RNC
                            );

                            var empresaCreada = new Empresa
                            {
                                Id = entity.Id,
                                RNC = entity.RNC,
                                Nombre = entity.Nombre,
                                NombreComercial = entity.NombreComercial,
                                Direccion = entity.Direccion,
                                Telefono = entity.Telefono,
                                Email = entity.Email
                            };

                            return empresaCreada;
                        }
                        else
                        {
                            _logger.LogWarning(
                                "No se pudo crear la empresa con RNC: {RNC}",
                                entity.RNC
                            );

                            return null;
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

        public async Task<Empresa> DeleteEmpresaAsync(int empresaId)
        {
            try
            {
                using (SqlConnection connection = await _sqlConnectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Empresa_Eliminar", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", empresaId);

                      

                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Empresa desactivda exitosamente");

                            return new Empresa
                            {
                                Id = empresaId
                            };
                        }

                        _logger.LogWarning(
                            "No se pudo desactivar la empresa con ID: {empresaId}", empresaId);

                        return null;
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error al desactivar la empresa");
                throw;
            };
        }

        public async Task<List<Empresa>> GetAllEmpresaAsync()
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_ObtenerTodos");
                using (SqlConnection connection = await _sqlConnectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Empresa_ObtenerTodos", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;


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
                                    Email = reader.GetString(reader.GetOrdinal("Email"))
                                };
                                empresas.Add(empresa);
                            }

                            if (!empresas.Any()) 
                            {
                                _logger.LogInformation("No se encontraron empresas en la base de datos");
                                return null;
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
                return new List<Empresa>();
            }
        }

        public async Task<Empresa> GetEmpresaByIdAsync(int empresaId)
        {
            try 
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_ObtenerPorId con ID: {EmpresaId}", empresaId);

                using (SqlConnection connection = await _sqlConnectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Empresa_ObtenerPorId", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", empresaId);

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
                                    Email = reader.GetString(reader.GetOrdinal("Email"))
                                };
                                _logger.LogInformation("Empresa con ID: {EmpresaId} obtenida exitosamente", empresaId);
                                return empresa;
                            }
                            else 
                            {
                                _logger.LogWarning("No se encontró la empresa con ID: {EmpresaId}", empresaId);
                                return null;
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

        public async Task<Empresa> UpdateEmpresaAsync(Empresa entity)
        {
            try
            {
                _logger.LogInformation("Ejecucion del proceso almacenado sp_Empresa_Actualizar con ID: {EmpresaId}", entity.Id);

                using (SqlConnection connection = await _sqlConnectionFactory.CreateConnectionAsync())
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


                        var result = await command.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 1)
                        {
                            _logger.LogInformation("Empresa actualizada exitosamente");

                            return entity;
                        }

                        _logger.LogWarning(
                            "No se pudo actualizar la empresa con ID: {Id}", entity.Id);

                        return null;
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
