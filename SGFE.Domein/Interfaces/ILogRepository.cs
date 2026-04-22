

using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface ILogRepository
    {
        Task CreateLogsSistemaAsync(Log entity);
    }
}
