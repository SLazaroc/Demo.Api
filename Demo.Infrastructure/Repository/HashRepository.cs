using Demo.Domain.Model;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace Demo.Infrastructure.Repository
{
    public static class HashRepository
    {
        private static IDictionary<int, Hash> _hash = new Dictionary<int, Hash>();

        public static bool IsShutdown;

        public static void Add(Hash entity)
        {
            if (!_hash.ContainsKey(entity.Id))
            {
                _hash.Add(entity.Id, entity);
            }
        }

        public static IEnumerable<Hash> AsNoTracking(bool isEncrypted)
        {
            return _hash.Values.Where(x => x.IsEncrypted == isEncrypted);
        }

        public static Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public static bool Exists(int id)
        {
            return _hash.ContainsKey(id);
        }

        public static Hash Get(int id)
        {
            if (_hash.ContainsKey(id))
            {
                return (Hash)_hash[id];
            }
            else
            {
                return new Hash();
            }    
        }

        public static void Update(Hash entity)
        {
            if (_hash.ContainsKey(entity.Id))
            {
                _hash[entity.Id] = entity;
            }
        }

        public static int GetCount()
        {
            return _hash.Count();
        }
    }
}
