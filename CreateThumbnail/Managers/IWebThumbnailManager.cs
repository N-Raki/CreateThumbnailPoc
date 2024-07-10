using System.Drawing;

namespace CreateThumbnail.Managers;

public interface IWebThumbnailManager
{
    Task<byte[]?> CreateWebThumbnailAsync(int id);
}