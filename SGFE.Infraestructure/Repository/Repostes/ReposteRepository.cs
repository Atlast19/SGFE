using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys.ReportesEntirys;
using SGFE.Domein.Interfaces.Reportes;

namespace SGFE.Percistence.Repository.Repostes
{
    public class ReposteRepository : IReporteRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<ReposteRepository> _logger;


        public ReposteRepository(ISqlConnectionFactory connectionFactory, ILogger<ReposteRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;

        }
        public async Task<List<FacturaRepostes>> GetFacturaRepostesAsync(FacturaRepostes filtro)
        {
            try 
            {
                _logger.LogInformation($"Ejecucion del proceso almacenado sp_Reporte_Facturas para la empresa {filtro.EmpresaId} desde {filtro.FechaDesde} hasta {filtro.FechaHasta}");
                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Reporte_Facturas", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpresaId", filtro.EmpresaId);
                        command.Parameters.AddWithValue("@FechaDesde", filtro.FechaDesde);
                        command.Parameters.AddWithValue("@FechaHasta", filtro.FechaHasta);


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
                                        FechaHasta = reader.GetDateTime(reader.GetOrdinal("FechaHasta"))
                                    };
                                    facturaList.Add(factura);
                                }
                                _logger.LogInformation($"Proceso almacenado sp_Reporte_Facturas ejecutado exitosamente para la empresa {filtro.EmpresaId} desde {filtro.FechaDesde} hasta {filtro.FechaHasta}. Se obtuvieron {facturaList.Count} registros.");
                                return facturaList;
                            }
                            else
                            {
                                _logger.LogInformation("No se encontraron los datos solicitados");
                                return null;
                                //throw new ArgumentException("No se encontraron los datos solicitados");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el reporte de facturas para la empresa {filtro.EmpresaId} desde {filtro.FechaDesde} hasta {filtro.FechaHasta}");
                throw;
            }
        }

        public async Task<List<ResumenFacturas>> GetResumenFacturasAsync(ResumenFacturas filtro)
        {
            try
            {
                _logger.LogInformation($"Ejecucion del proceso almacenado sp_Reporte_ResumenFacturaspara la empresa {filtro.EmpresaId} en el año {filtro.Anio} y mes {filtro.Mes}");

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand command = new SqlCommand("sp_Reporte_ResumenFacturas", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpresaId", filtro.EmpresaId);
                        command.Parameters.AddWithValue("@Anio", filtro.Anio);
                        command.Parameters.AddWithValue("@Mes", filtro.Mes);

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
                                _logger.LogInformation($"Proceso almacenado sp_Reporte_ResumenFacturas ejecutado exitosamente para la empresa {filtro.EmpresaId} en el año {filtro.Anio} y mes {filtro.Mes}. Se obtuvieron {resumenList.Count} registros.");
                                return resumenList;
                            }
                            else {
                                _logger.LogInformation("No se encontraron los datos solicitados");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el resumen de facturas para la empresa {filtro.EmpresaId} en el año {filtro.Anio} y mes {filtro.Mes}");
                throw;
            }
        }
    }
}
