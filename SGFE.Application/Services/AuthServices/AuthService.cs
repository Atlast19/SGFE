
using Microsoft.AspNetCore.Identity;
using SGFE.Application.Services.AuthService;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Usuarios;

namespace SGFE.Application.Services.AuthServices
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repo;
        private readonly JwtService _jwt;
        private readonly PasswordHasher<Usuario> _hasher;

        public AuthService(IUsuarioRepository repo, JwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
            _hasher = new PasswordHasher<Usuario>();
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _repo.GetEmailForLogin(email);

            if (user == null)
                throw new Exception("Usuario no existe");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Credenciales inválidas");

            //  obtener roles
            var roles = await _repo.GetRolesByUsuarioIdAsync(user.Id);

            // 🔥 generar token con roles
            return _jwt.GenerateToken(user, roles);
        }
    }
}
