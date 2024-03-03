using Rainfall.Web.API;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
       .Build();

    
  
            CreateHostBuilder(args).Build().Run();
   
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}