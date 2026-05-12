
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SGFE.Application.Interfaces.SessionContext;
using System.Security.Claims;

namespace SGFE.Percistence.Contexts.SessionContext
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SqlConnectionFactory(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SqlConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));

            await connection.OpenAsync();

            // Obtener claims del JWT
            var usuarioId = _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var empresaId = _httpContextAccessor.HttpContext?
                .User.FindFirst("EmpresaId")?.Value;

            // Configurar SESSION_CONTEXT
            using (SqlCommand cmd = new SqlCommand(@"
            EXEC sp_set_session_context @key=N'UsuarioId', @value=@UsuarioId;
            EXEC sp_set_session_context @key=N'EmpresaId', @value=@EmpresaId;
        ", connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioId",
                    (object?)usuarioId ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@EmpresaId",
                    (object?)empresaId ?? DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
            }

            return connection;
        }
    }
}
