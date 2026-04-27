
using Microsoft.AspNetCore.Identity;
using SGFE.Application.Interfaces.PassworkHash;
using SGFE.Domein.Entitys;

namespace SGFE.Application.Services.PassworkHash
{
    public class PassworkHashSercie : IPassworkHashService
    {
        private readonly PasswordHasher<Usuario> _hasher = new();

        public string Hash(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool Verify(string hash, string password)
        {
            var result = _hasher.VerifyHashedPassword(null, hash, password);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
