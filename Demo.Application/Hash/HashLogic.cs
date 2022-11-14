using Demo.Domain.Hash;
using Demo.Domain.Model;
using Demo.Infrastructure.Common;
using Demo.Infrastructure.Exceptions;
using Demo.Infrastructure.Repository;
using System.Data;

namespace Demo.Application.Hash
{
    public class HashLogic : IHashLogic
    {
        public async Task<HashResult> GenerateHash(string password)
        {
            ValidateServerState();
            var id = GenerateId();
            HashRepository.Add(new Domain.Model.Hash() { Id = id, SHA512 = password });
            return new HashResult() { Id = id };
        }

        public async Task<GetHashResult> Get(int id)
        {
            ValidateServerState();
            if (!HashRepository.Exists(id)) throw new CustomArgumentException(Constants.Message.M1003[0], Constants.Message.M1003[1]);

            var hash = HashRepository.Get(id);
            return new GetHashResult() { SH512 = hash.SHA512 };
        }

        public async Task<HashStats> GetStats()
        {
            ValidateServerState();
            var hashs = HashRepository.AsNoTracking(true);
            var stats = new HashStats();
            stats.Total = hashs.Count();
            stats.Average = stats.Total != 0 ? (int)(hashs.Sum(x => x.ElapsedTime) / stats.Total) : 0;
            return stats;

        }

        public async Task<ShutdownResult> StartOrShutdown(bool isShutdown)
        {
            HashRepository.IsShutdown = isShutdown;
            return new ShutdownResult() { Message = isShutdown ? Constants.Message.M5000[1] : Constants.Message.M5001[1] };
        }


        private void ValidateServerState()
        {
            if (HashRepository.IsShutdown)
            {
                throw new CustomArgumentException(Constants.Message.M1002[0], Constants.Message.M1002[1]);
            }
        }
        private int GenerateId()
        {
            var countHash = HashRepository.GetCount();
            return countHash + 1;
        }
    }
}
