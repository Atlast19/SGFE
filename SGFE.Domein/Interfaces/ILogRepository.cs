

using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface ILogRepository
    {
        Task CreateLogsSistemaAsync(Log entity);
        Task<List<Log>> GetLogsByFiltrosAsync(int? empresaId, string nivel, DateTime? desde, DateTime? hasta);
    }
}
