
namespace SGFE.Application.Interfaces.PassworkHash
{
    public interface IPassworkHashService
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
