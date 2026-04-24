using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces;
using System.Data;

namespace SGFE.Percistence.Repository.Facturas
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FacturaRepository> _logger;
        private readonly string _connectionString;  

        public FacturaRepository(IConfiguration configuration, ILogger<FacturaRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task CreateFacturaAsync(Factura factura, List<FacturaDetalle> detalles)
        {
            try
            {
                _logger.LogInformation("Ejecutando sp_Factura_Crear");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_Factura_Crear", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@EmpresaId", factura.EmpresaId);
                    cmd.Parameters.AddWithValue("@ClienteId", factura.ClienteId);
                    cmd.Parameters.AddWithValue("@TipoECFId", factura.TipoECFId);
                    cmd.Parameters.AddWithValue("@FechaEmision", factura.FechaEmision == DateTime.MinValue ? (object)DBNull.Value : factura.FechaEmision);

                    // Parámetro tipo tabla para detalles
                    var detallesTable = new DataTable();
                    detallesTable.Columns.Add("Descripcion", typeof(string));
                    detallesTable.Columns.Add("Cantidad", typeof(decimal));
                    detallesTable.Columns.Add("PrecioUnitario", typeof(decimal));
                    detallesTable.Columns.Add("Monto", typeof(decimal));
                    detallesTable.Columns.Add("Itbis", typeof(decimal));
                    detallesTable.Columns.Add("Descuento", typeof(decimal));

                    foreach (var detalle in detalles)
                    {
                        decimal monto = detalle.Cantidad * detalle.PrecioUnitario;
                        detallesTable.Rows.Add(
                            detalle.Descripcion,
                            detalle.Cantidad,
                            detalle.PrecioUnitario,
                            monto,
                            detalle.Itbis,
                            detalle.Descuento
                        );
                    }

                    var tvpParam = cmd.Parameters.AddWithValue("@Detalles", detallesTable);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.FacturaDetalleType";

                   
                    var facturaIdOut = cmd.Parameters.Add("@FacturaId", SqlDbType.Int);
                    facturaIdOut.Direction = ParameterDirection.Output;
                    var ncfOut = cmd.Parameters.Add("@NCF", SqlDbType.NVarChar, 19);
                    ncfOut.Direction = ParameterDirection.Output;

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    
                    _logger.LogInformation("Factura creada con Id: {FacturaId}, NCF: {NCF}", facturaIdOut.Value, ncfOut.Value);
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
                _logger.LogInformation("Ejecutando sp_Factura_ObtenerPorId para FacturaId: {FacturaId}", FacturaId);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Factura_ObtenerPorId", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FacturaId", FacturaId);
                        
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            Factura factura = null;
                            
                            if (await reader.ReadAsync())
                            {
                                factura = new Factura
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    NCF = reader.GetString(reader.GetOrdinal("NCF")),
                                    FechaEmision = reader.GetDateTime(reader.GetOrdinal("FechaEmision")),
                                    MontoTotal = reader.GetDecimal(reader.GetOrdinal("MontoTotal")),
                                    ItbisTotal = reader.GetDecimal(reader.GetOrdinal("ItbisTotal")),
                                    Estado = reader.GetString(reader.GetOrdinal("Estado")),
                                    TrackId = reader.IsDBNull(reader.GetOrdinal("TrackId")) ? null : reader.GetString(reader.GetOrdinal("TrackId")),
                                };
                            }
                            if (factura == null)
                            {
                                _logger.LogError("No se encontró factura con Id: {FacturaId}", FacturaId);
                                throw new ArgumentNullException($"No se encontró factura con Id: {FacturaId}");
                            }
                            
                            if (await reader.NextResultAsync())
                            {
                                var detalles = new List<FacturaDetalle>();
                                while (await reader.ReadAsync())
                                {
                                    var detalle = new FacturaDetalle
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                        Cantidad = reader.GetDecimal(reader.GetOrdinal("Cantidad")),
                                        PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                                        Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                                        Itbis = reader.GetDecimal(reader.GetOrdinal("Itbis")),
                                        Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento"))
                                    };
                                    detalles.Add(detalle);
                                }
                                factura.FacturaDetalles = detalles; 
                            }
                            return factura;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener factura por Id: {FacturaId}", FacturaId);
                throw;
            }
        }

        public async Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio)
        {
            try
            {
                _logger.LogInformation("Ejecutando sp_Factura_ActualizarDGIIResponse para FacturaId: {FacturaId}, TrackId: {TrackId}, Estado: {Estado}", facturaId, trackId, estado);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Factura_ActualizarDGIIResponse", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FacturaId", facturaId);
                        cmd.Parameters.AddWithValue("@TrackId", trackId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", estado);
                        cmd.Parameters.AddWithValue("@RespuestaDGII", respuestaDGII ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaEnvio", fechaEnvio ?? (object)DBNull.Value);
                        
                        await connection.OpenAsync();
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

                using (SqlConnection connection = new SqlConnection(_connectionString)) 
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Factura_ActualizarEstado", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FacturaId", facturaId);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        await connection.OpenAsync();
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
