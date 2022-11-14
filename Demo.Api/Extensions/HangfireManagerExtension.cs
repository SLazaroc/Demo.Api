using Demo.Application.Manager;
using Hangfire;

namespace Demo.Api.Extensions
{
    public static class HangfireManagerExtension
    {
        public static void AddHangfireService(this IApplicationBuilder app, IConfiguration configuration)
        {
            bool enableJob = bool.TryParse(configuration["HashJobSettings:AllowHangfire"], out enableJob);
            if (enableJob)
            {
                var hangfireCronExpression = configuration["HashJobSettings:HangfireCronExpression"];
                var hangfireTimeZone = configuration["HashJobSettings:HangfireTimeZone"];
                RecurringJob.AddOrUpdate<IJobManager>(
                    job => job.ProcessHash(),
                    @hangfireCronExpression,
                    TimeZoneInfo.FindSystemTimeZoneById(hangfireTimeZone));
            }
        }
    }
}
