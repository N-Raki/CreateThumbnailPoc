using CefSharp;
using CefSharp.OffScreen;
using CreateThumbnail.Managers;
using CreateThumbnail.Services;

namespace CreateThumbnail;

public static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var settings = new CefSettings();
            Cef.Initialize(settings);
        
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            Cef.Shutdown();
        }
    }
    
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<ThumbnailServiceImpl>();
                services.AddScoped<IWebThumbnailManager, WebThumbnailManager>();
                services.AddScoped<ICefSharpHelper, CefSharpHelper>();
                services.AddHostedService<Worker>();
                services.AddLogging(config =>
                {
                    config.AddConsole();
                });
            });
}

