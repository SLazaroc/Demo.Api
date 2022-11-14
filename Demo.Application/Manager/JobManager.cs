using Demo.Application.Core;
using Demo.Infrastructure.Common;
using Demo.Infrastructure.Exceptions;
using Demo.Infrastructure.Repository;
using Demo.Infrastructure.Services;

namespace Demo.Application.Manager
{
    public class JobManager : IJobManager
    {
        private readonly ITraceLog logger;
        public JobManager(
            ITraceLog logger)
        {
            this.logger = logger;
        }
        public void ProcessHash()
        {
            Console.WriteLine($"process hash: {DateTime.Now}");
            this.logger.LogInfo(int.Parse(Constants.Message.M5002[0]), Constants.Message.M5002[1], $"{Constants.Message.M5002[1]}");

            if (HashRepository.IsShutdown)
            {
                this.logger.LogInfo(int.Parse(Constants.Message.M1002[0]), Constants.Message.M1002[1], Constants.Message.M1002[1]);
            }
            else
            {
                var items = HashRepository.AsNoTracking(false);
                foreach (var item in items)
                {
                    DateTime dt1 = DateTime.Now;
                    item.SHA512 = Encrypt.ConvertToSHA512(item.SHA512);
                    item.IsEncrypted = true;
                    DateTime dt2 = DateTime.Now;
                    item.ElapsedTime = (dt2.Ticks - dt1.Ticks) * 100;
                    HashRepository.Update(item);
                }
            }
        }
    }
}
