using Demo.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Demo.Infrastructure.Common.Constants;

namespace Demo.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddAInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITraceLog, TraceLog>();
        }
    }
}
