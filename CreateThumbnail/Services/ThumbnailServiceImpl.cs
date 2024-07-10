using CreateThumbnail.Managers;
using Grpc.Core;
using CreateThumbnailService;
using Google.Protobuf;

namespace CreateThumbnail.Services;

internal sealed class ThumbnailServiceImpl : ThumbnailService.ThumbnailServiceBase
{
    private readonly IWebThumbnailManager _webThumbnailManager;
    
    public ThumbnailServiceImpl(IWebThumbnailManager webThumbnailManager)
    {
        _webThumbnailManager = webThumbnailManager;
    }
    
    public override async Task<ThumbnailResponse> CreateThumbnail(ThumbnailRequest request, ServerCallContext context)
    {
        // TODO: Get media type from request.MediaId
        
        const string mediaType = "web";

        if (mediaType == "web")
        {
            using var memoryStream = new MemoryStream();
            var thumbnail = await _webThumbnailManager.CreateWebThumbnailAsync(request.MediaId);
            return new ThumbnailResponse
            {
                Thumbnail = ByteString.CopyFrom(thumbnail)
            };
        }
    }
}