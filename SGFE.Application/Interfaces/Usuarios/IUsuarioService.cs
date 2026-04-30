using SGFE.Application.Models.Usuarios;

namespace SGFE.Application.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task<CreateUsuarioModel> CreateUsuarioAsync(CreateUsuarioModel model);
        Task<UpdateUsuarioModel> UpdateUsuarioAsync(UpdateUsuarioModel entity);
        Task<GetUsuarioModel> GetUsuarioByIdAsync(int id);
        Task<GetUsuarioModel> GetUsuarioByEmailAsync(string email);
        Task<List<GetUsuarioModel>> GetAllUsuarioAsync();
        Task<GetUsuarioModel> DeleteUsuarioAsync(int Id);
        Task<List<string>> GetRolesByUsuarioIdAsync(int usuarioId);
        Task<LoginRequestModel> GetEmailForLogin(string email);
    }
}
