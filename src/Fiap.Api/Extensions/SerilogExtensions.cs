using Serilog.Formatting.Json;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SerilogExtensions
    {
        public static void ConfigureSerilog(HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration)
        {
            configuration
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Fiap.Api")
                .WriteTo.Console()
                .WriteTo.File(
                    new JsonFormatter(renderMessage: true),
                    "logs/log-.json",
                    rollingInterval: RollingInterval.Day);
        }
    }
}
