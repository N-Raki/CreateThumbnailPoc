using CreateThumbnailService;
using Grpc.Net.Client;

namespace BackOffice.Clients;

public class GrpcClient
{
    public ThumbnailService.ThumbnailServiceClient Client { get; }
    
    public GrpcClient()
    {
        var channel = GrpcChannel.ForAddress("http://127.0.0.1:5001");
        Client = new ThumbnailService.ThumbnailServiceClient(channel);
    }
}