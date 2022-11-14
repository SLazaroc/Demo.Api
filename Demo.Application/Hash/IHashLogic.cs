using Demo.Domain.Hash;
using Demo.Domain.Model;

namespace Demo.Application.Hash
{
    public interface IHashLogic
    {
        Task<HashResult> GenerateHash(string password);
        Task<GetHashResult> Get(int id);
        Task<HashStats> GetStats();
        Task<ShutdownResult> StartOrShutdown(bool isShutdown);
    }
}
