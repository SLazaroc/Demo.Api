using Demo.Api.Code;
using Demo.Api.Extensions;
using Demo.Application;
using Demo.Infrastructure;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddEventLog(new Microsoft.Extensions.Logging.EventLog.EventLogSettings
    {
        SourceName = "Demo.Api",
        LogName = "Application"
    });
});

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));


builder.Services.AddApplication(builder.Configuration);
builder.Services.AddAInfrastructure(builder.Configuration);

builder.Services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

builder.Services.AddHangfireServer();

builder.Services.AddSwaggerGen(c =>
{
    static string SchemaIdStrategy(Type currentClass)
    {
        return currentClass.Name.EndsWith("Models") ? currentClass.Name.Replace("Model", string.Empty) : currentClass.Name;
    }
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo.Api", Version = "v1" });
    c.CustomSchemaIds(SchemaIdStrategy);
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header \"Authorization: Bearer {token}\""

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"

                }
            }, Array.Empty<string>()
        }
    });
    c.CustomOperationIds(d =>
    {
        var actionDescriptor = d.ActionDescriptor as ControllerActionDescriptor;
        return $"{actionDescriptor?.ControllerName}_{actionDescriptor?.ActionName}";
    });
    c.OperationFilter<AuthorizationHeaderParameter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseHangfireDashboard();
app.AddHangfireService(builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
