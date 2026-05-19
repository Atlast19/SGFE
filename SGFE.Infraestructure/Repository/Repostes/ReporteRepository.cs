using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys.ReportesEntirys;
using SGFE.Domein.Interfaces.Reportes;
using System.Data;

namespace SGFE.Percistence.Repository.Repostes
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<ReporteRepository> _logger;


        public ReporteRepository(ISqlConnectionFactory connectionFactory, ILogger<ReporteRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;

        }
        public async Task<List<FacturaReportes>> GetFacturaRepostesAsync(FacturaReportes filtro)
        {
            try
            {
                _logger.LogInformation("Ejecutando sp_Reporte_Facturas para la empresa {EmpresaId}",filtro.EmpresaId);

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand command = new SqlCommand("sp_Reporte_Facturas", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@EmpresaId", filtro.EmpresaId);
                        command.Parameters.AddWithValue("@FechaEmision", filtro.FechaEmision.Date);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            var facturaList = new List<FacturaReportes>();

                            while (await reader.ReadAsync())
                            {
                                if (reader.FieldCount == 1 &&
                                    reader[0] != DBNull.Value &&
                                    Convert.ToInt32(reader[0]) == 0)
                                {
                                    _logger.LogWarning("No se encontraron facturas para la empresa {EmpresaId}",filtro.EmpresaId);
                                    return null;
                                }

                                FacturaReportes factura = new FacturaReportes
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    EmpresaId = reader.GetInt32(reader.GetOrdinal("EmpresaId")),
                                    NCF = reader.GetString(reader.GetOrdinal("NCF")),
                                    FechaEmision = reader.GetDateTime(reader.GetOrdinal("FechaEmision")),
                                    MontoTotal = reader.GetDecimal(reader.GetOrdinal("MontoTotal")),
                                    ItbisTotal = reader.GetDecimal(reader.GetOrdinal("ItbisTotal")),
                                    SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                                    Estado = reader.GetString(reader.GetOrdinal("Estado")),
                                    TrackId = reader["TrackId"]?.ToString(),
                                    Cliente = reader.GetString(reader.GetOrdinal("Cliente")),
                                    ClienteDocumento = reader.GetString(reader.GetOrdinal("ClienteDocumento")),
                                    TipoComprobante = reader.GetString(reader.GetOrdinal("TipoComprobante")),

                                    // DATOS RESUMEN
                                    Anio = reader.GetInt32(reader.GetOrdinal("Anio")),
                                    Mes = reader.GetInt32(reader.GetOrdinal("Mes")),
                                    CantidadFacturas = reader.GetInt32(reader.GetOrdinal("CantidadFacturas")),
                                    TotalFacturado = reader.GetDecimal(reader.GetOrdinal("TotalFacturado")),
                                    TotalItbis = reader.GetDecimal(reader.GetOrdinal("TotalItbis"))
                                };

                                facturaList.Add(factura);
                            }

                            if (!facturaList.Any())
                            {
                                _logger.LogWarning("No se encontraron registros para la empresa {EmpresaId}",filtro.EmpresaId);
                                return null;
                            }

                            _logger.LogInformation("Reporte generado correctamente para la empresa {EmpresaId}. Registros obtenidos: {Cantidad}",filtro.EmpresaId,facturaList.Count);
                            return facturaList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error al ejecutar sp_Reporte_Facturas para la empresa {EmpresaId}",filtro.EmpresaId);
                throw;
            }
        }

    }
}
