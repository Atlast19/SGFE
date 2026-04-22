
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IConfiguracionRepository
    {
        Task<List<Configuracion>> GetConfiguracion(string SPname);
        Task UpsetConfiguracion(string SPname);
    }
}
