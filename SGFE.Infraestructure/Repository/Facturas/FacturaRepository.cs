using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SGFE.Application.Interfaces.SessionContext;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Facturas;
using System.Data;

namespace SGFE.Percistence.Repository.Facturas
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<FacturaRepository> _logger;


        public FacturaRepository(ISqlConnectionFactory connectionFactory, ILogger<FacturaRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async Task<(int facturaId, string ncf)> CreateFacturaAsync(Factura factura, List<FacturaDetalle> detalles)
        {
            try
            {
                _logger.LogInformation("Ejecutando sp_Factura_Crear");

                using (SqlConnection conn = await _connectionFactory.CreateConnectionAsync())
                using (SqlCommand cmd = new SqlCommand("sp_Factura_Crear", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpresaId", factura.EmpresaId);
                    cmd.Parameters.AddWithValue("@ClienteId", factura.ClienteId);
                    cmd.Parameters.AddWithValue("@TipoECFId", factura.TipoECFId);

                    // TVP (MUY IMPORTANTE)
                    var table = new DataTable();
                    table.Columns.Add("Descripcion", typeof(string));
                    table.Columns.Add("Cantidad", typeof(decimal));
                    table.Columns.Add("PrecioUnitario", typeof(decimal));
                    table.Columns.Add("Monto", typeof(decimal));
                    table.Columns.Add("Itbis", typeof(decimal));
                    table.Columns.Add("Descuento", typeof(decimal));

                    foreach (var d in detalles)
                    {
                        table.Rows.Add(d.Descripcion, d.Cantidad, d.PrecioUnitario, d.Monto, d.Itbis, d.Descuento);
                    }

                    var paramDetalles = cmd.Parameters.AddWithValue("@Detalles", table);
                    paramDetalles.SqlDbType = SqlDbType.Structured;
                    paramDetalles.TypeName = "dbo.FacturaDetalleType";

                    // OUTPUTS
                    var paramFacturaId = new SqlParameter("@FacturaId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var paramNCF = new SqlParameter("@NCF", SqlDbType.NVarChar, 19)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(paramFacturaId);
                    cmd.Parameters.Add(paramNCF);

                    await cmd.ExecuteNonQueryAsync();

                    return ((int)paramFacturaId.Value, paramNCF.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear factura");
                throw;
            }
        }

        public async Task<Factura> GetfacturaByIdAsync(int FacturaId)
        {
            try
            {
                Factura factura = null;
                

                using (SqlConnection conn = await _connectionFactory.CreateConnectionAsync())
                using (SqlCommand cmd = new SqlCommand("sp_Factura_ObtenerPorId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FacturaId", FacturaId);


                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // 1. CABECERA
                        if (await reader.ReadAsync())
                        {
                            factura = new Factura
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NCF = reader.GetString(reader.GetOrdinal("NCF")),
                                TipoECFId = reader.GetInt32(reader.GetOrdinal("TipoECFId")),
                                TiposECF = reader.GetString(reader.GetOrdinal("Codigo")),
                                FechaEmision = reader.GetDateTime(reader.GetOrdinal("FechaEmision")),
                                MontoTotal = reader.GetDecimal(reader.GetOrdinal("MontoTotal")),
                                ItbisTotal = reader.GetDecimal(reader.GetOrdinal("ItbisTotal")),
                                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                                Estado = reader.GetString(reader.GetOrdinal("Estado")),
                                TrackId = reader["TrackId"]?.ToString(),

                                ClienteNombre = reader["Nombre"]?.ToString(),
                                ClienteDocumento = reader["Documento"]?.ToString(),

                                FacturaDetalles = new List<FacturaDetalle>()
                            };
                        }

                        // 2. DETALLES
                        if (factura != null && await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                factura.FacturaDetalles.Add(new FacturaDetalle
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Cantidad = reader.GetDecimal(reader.GetOrdinal("Cantidad")),
                                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                                    Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                                    Itbis = reader.GetDecimal(reader.GetOrdinal("Itbis")),
                                    Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento"))
                                });
                            }
                        }
                    }
                }

                return factura;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener factura por Id: {FacturaId}", FacturaId);
                throw;
            }
        }

        public async Task GuardarXmlAsync(int facturaId, string xml)
        {
            try
            {
                using (SqlConnection conn = await _connectionFactory.CreateConnectionAsync())
                using (SqlCommand cmd = new SqlCommand("sp_Factura_GuardarXml", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FacturaId", facturaId);
                    cmd.Parameters.AddWithValue("@XmlGenerado", xml);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar XML de la factura");
                throw;
            }
        }

        public async Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio)
        {
            try
            {
                _logger.LogInformation("Ejecutando sp_Factura_ActualizarDGIIResponse para FacturaId: {FacturaId}, TrackId: {TrackId}, Estado: {Estado}", facturaId, trackId, estado);

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Factura_ActualizarDGIIResponse", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FacturaId", facturaId);
                        cmd.Parameters.AddWithValue("@TrackId", trackId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", estado);
                        cmd.Parameters.AddWithValue("@RespuestaDGII", respuestaDGII ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaEnvio", fechaEnvio ?? (object)DBNull.Value);
                        
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar respuesta DGII de factura {FacturaId}", facturaId);
                throw;
            }
        }

        public async Task UpdateFacturaEstado(int facturaId, string estado)
        {
            try
            {
                _logger.LogInformation("Ejecutando sp_Factura_ActualizarEstado para FacturaId: {FacturaId}, Estado: {Estado}", facturaId, estado);

                using (SqlConnection connection = await _connectionFactory.CreateConnectionAsync()) 
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Factura_ActualizarEstado", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FacturaId", facturaId);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estado de factura {FacturaId}", facturaId);
                throw;
            }
        }
    }
}
