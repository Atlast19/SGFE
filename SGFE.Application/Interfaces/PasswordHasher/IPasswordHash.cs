
namespace SGFE.Application.Interfaces.PasswordHasher
{
    public interface IPasswordHash
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
