using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys.ReportesEntirys;
using SGFE.Domein.Interfaces.Reportes;

namespace SGFE.Percistence.Repository.Repostes
{
    public class ReposteRepository : IReporteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReposteRepository> _logger;
        private readonly string _connectionString;

        public ReposteRepository(IConfiguration configuration, ILogger<ReposteRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<FacturaRepostes>> GetFacturaRepostesAsync(int empresaId, DateTime? fechaDesde, DateTime? fechaHasta, string estado)
        {
            try 
            {
                _logger.LogInformation($"Ejecucion del proceso almacenado sp_Reporte_Facturas para la empresa {empresaId} desde {fechaDesde} hasta {fechaHasta} con estado {estado}");
                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Reporte_Facturas", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpresaId", empresaId);
                        command.Parameters.AddWithValue("@FechaDesde", fechaDesde);
                        command.Parameters.AddWithValue("@FechaHasta", fechaHasta);
                        command.Parameters.AddWithValue("@Estado", estado);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                var facturaList = new List<FacturaRepostes>();

                                while (await reader.ReadAsync())
                                {
                                    FacturaRepostes factura = new FacturaRepostes
                                    {
                                        EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId")),
                                        FechaDesde = reader.GetDateTime(reader.GetOrdinal("FechaDesde")),
                                        FechaHasta = reader.GetDateTime(reader.GetOrdinal("FechaHasta")),
                                        Estado = reader.GetBoolean(reader.GetOrdinal("Estado"))
                                    };
                                    facturaList.Add(factura);
                                }
                                _logger.LogInformation($"Proceso almacenado sp_Reporte_Facturas ejecutado exitosamente para la empresa {empresaId} desde {fechaDesde} hasta {fechaHasta} con estado {estado}. Se obtuvieron {facturaList.Count} registros.");
                                return facturaList;
                            }
                            else
                            {
                                _logger.LogInformation("No se encontraron los datos solicitados");
                                throw new ArgumentException("No se encontraron los datos solicitados");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el reporte de facturas para la empresa {empresaId} desde {fechaDesde} hasta {fechaHasta} con estado {estado}");
                throw;
            }
        }

        public async Task<List<ResumenFacturas>> GetResumenFacturasAsync(int empresaId, int? anio, int? mes)
        {
            try
            {
                _logger.LogInformation($"Ejecucion del proceso almacenado sp_Reporte_ResumenFacturaspara la empresa {empresaId} en el año {anio} y mes {mes}");

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Reporte_ResumenFacturas", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpresaId", empresaId);
                        command.Parameters.AddWithValue("@Anio", anio);
                        command.Parameters.AddWithValue("@Mes", mes);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                var resumenList = new List<ResumenFacturas>();

                                while (await reader.ReadAsync())
                                {
                                    ResumenFacturas resumen = new ResumenFacturas
                                    {
                                        EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId")),
                                        Anio = reader.GetInt32(reader.GetOrdinal("Anio")),
                                        Mes = reader.GetInt32(reader.GetOrdinal("Mes"))
                                    };
                                    resumenList.Add(resumen);
                                }
                                _logger.LogInformation($"Proceso almacenado sp_Reporte_ResumenFacturas ejecutado exitosamente para la empresa {empresaId} en el año {anio} y mes {mes}. Se obtuvieron {resumenList.Count} registros.");
                                return resumenList;
                            }
                            else {
                                _logger.LogInformation("No se encontraron los datos solicitados");
                                throw new ArgumentException("No se encontraron los datos solicitados");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el resumen de facturas para la empresa {empresaId} en el año {anio} y mes {mes}");
                throw;
            }
        }
    }
}
