using Destructurama;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;
using System.IO;

namespace Rainfall.SharedLibrary.ExtensionMethods
{
    public static class SerilogConfigurator
    {
        public static Serilog.ILogger CreateLogger(IConfiguration configuration)
        {
            string logPath = configuration["Log:Path"];
            string retainedFileCountLimit = configuration["Log:RetainedFileCountLimit"];

            if (string.IsNullOrEmpty(logPath))
            {
                logPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Logs", ".log");
            }
            else
            {
                logPath = $"{configuration["Log:Path"]}/.log";
            }

            LoggerConfiguration logger = new LoggerConfiguration()
                .Destructure.UsingAttributes()
                .Enrich.WithProperty("Environment", configuration["Log:Environment"])
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Query", LogEventLevel.Error);

            if (configuration["Log:EnabledGrafanaLogging"] == "true")
            {
                logger = logger.WriteTo.GrafanaLoki(configuration["Log:GrafanaUrl"]);
            }
            else
            {
                int.TryParse(retainedFileCountLimit, out int fileCountLimit);

                logger = logger.WriteTo.File(logPath,
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: fileCountLimit == 0 ? 50000 : fileCountLimit);
            }

            return logger.CreateLogger();
        }
    }
}
