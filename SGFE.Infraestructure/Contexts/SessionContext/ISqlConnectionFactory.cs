using Microsoft.Data.SqlClient;

namespace SGFE.Application.Interfaces.SessionContext
{
    public interface ISqlConnectionFactory
    {
        Task<SqlConnection> CreateConnectionAsync();
    }
}
