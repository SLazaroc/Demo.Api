using MediatR;
using Demo.Application.Hash;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Demo.Application.Manager;

namespace Demo.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IHashLogic, HashLogic>();
            services.AddTransient<IJobManager, JobManager>();
        }
    }
}