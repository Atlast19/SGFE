
using Microsoft.AspNetCore.Identity;
using SGFE.Application.Interfaces.PasswordHasher;

namespace SGFE.Application.Services.PasswordHasher
{
    public class PasswordHashService : IPasswordHash
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string Hash(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool Verify(string hash, string password)
        {
            var result = _hasher.VerifyHashedPassword(null, hash, password);

            return result == PasswordVerificationResult.Success
                || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
