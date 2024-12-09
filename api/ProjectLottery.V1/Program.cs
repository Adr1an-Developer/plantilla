using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading.Tasks;

namespace ProjectLottery.V1
{
    public class Program
  {
    //public Settings settings = Configuration.GetSection("Settings").Get<Settings>();
    private static async Task<int> Main(string[] args)
    {
      Console.Title = "Web Api - Default API";
      var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      var logConfig = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
          .MinimumLevel.Override("System", LogEventLevel.Warning)
          .Enrich.FromLogContext()
          .Enrich.WithExceptionDetails()
          .Enrich.WithMachineName();

      if (environment == EnvironmentName.Development)
        logConfig.WriteTo.Console(theme: AnsiConsoleTheme.Literate);
      else
        logConfig.WriteTo.Console(new ElasticsearchJsonFormatter());

      Log.Logger = logConfig.CreateLogger();

      try
      {
        Log.Information("Starting web host");
        var hotBuilder = CreateWebHostBuilder(args);
        await hotBuilder.Build().RunAsync();
        return 0;
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Web host terminated unexpectedly");
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost
          .CreateDefaultBuilder(args)
          .ConfigureLogging(builder => builder.ClearProviders())
          .UseStartup<Startup>()
          .UseSerilog();
    }

  }
}
