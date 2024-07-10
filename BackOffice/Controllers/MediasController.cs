using BackOffice.Clients;
using CreateThumbnailService;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediasController : ControllerBase
{
    [HttpPost("{id:int}/createThumbnail")]
    public async Task<IActionResult> CreateThumbnail(int id)
    {
        var client = new GrpcClient();
        var request = new ThumbnailRequest
        {
            MediaId = id
        };
        var response = await client.Client.CreateThumbnailAsync(request);
        var bytes = response.Thumbnail.ToByteArray();
        return File(bytes, "image/png");
    }
}