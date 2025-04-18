using Serilog.Formatting.Json;
using Serilog;

namespace Fiap.Api.Extensions
{
    public static class SerilogExtensions
    {
        public static void ConfigureSerilog(HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration)
        {
            configuration
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Fiap.Api")
                .WriteTo.Console(new JsonFormatter(renderMessage: true))
                .WriteTo.File(new JsonFormatter(renderMessage: true), "logs/log-.json", rollingInterval: RollingInterval.Day);
        }
    }
}
