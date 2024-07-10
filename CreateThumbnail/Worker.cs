using CreateThumbnail.Services;
using CreateThumbnailService;
using Grpc.Core;

namespace CreateThumbnail;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Server? _grpcServer;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Service is starting...");
        
        _grpcServer = new Server
        {
            Services = { ThumbnailService.BindService(_serviceProvider.GetRequiredService<ThumbnailServiceImpl>()) },
            Ports = { new ServerPort("0.0.0.0", 5001, ServerCredentials.Insecure) }
        };

        _grpcServer.Start();
        _logger.LogInformation("gRPC Server started on port 5001");

        await Task.Delay(Timeout.Infinite, stoppingToken);

        _logger.LogInformation("Service is stopping...");
        await _grpcServer.ShutdownAsync().WaitAsync(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service is stopping...");
        if (_grpcServer != null)
            await _grpcServer.ShutdownAsync();
        await base.StopAsync(cancellationToken);
    }
}