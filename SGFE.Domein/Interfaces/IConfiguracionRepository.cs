
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IConfiguracionRepository
    {
        Task<List<Configuracion>> GetConfiguracionAsync(int? empresaId, string clave);
        Task UpsetConfiguracionAync(int? empresaId, string clave, string valor, string descripcion);
    }
}
