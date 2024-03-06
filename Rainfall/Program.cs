using Rainfall.SharedLibrary.ExtensionMethods;
using Rainfall.Web.API;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

        try
        {

            Log.Logger = SerilogConfigurator.CreateLogger(configuration);
            Log.Information($"{configuration["Log:Application"]} is starting up...");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"{configuration["Log:Application"]} failed to start.");
        }
        finally
        {
            Log.Information($"{configuration["Log:Application"]}  has stopped.");
            Log.CloseAndFlush();
        }

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}