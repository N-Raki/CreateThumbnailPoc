using System.Drawing;
using CefSharp;

namespace CreateThumbnail.Managers;

internal sealed class WebThumbnailManager : IWebThumbnailManager
{
   private readonly ILogger<WebThumbnailManager> logger;
    
   public WebThumbnailManager(ILogger<WebThumbnailManager> logger)
   {
      this.logger = logger;
   }
    
   public async Task<byte[]?> CreateWebThumbnailAsync(int id)
   {
      try
      {
         using var _cefSharpHelper = new CefSharpHelper();
         await _cefSharpHelper.Browser.LoadUrlAsync("https://google.com");
         await _cefSharpHelper.Browser.WaitForInitialLoadAsync();
         return await _cefSharpHelper.Browser.CaptureScreenshotAsync();
         //using var devToolsClient = _cefSharpHelper.Browser.GetDevToolsClient();
         //var screenshot = await devToolsClient.Page.CaptureScreenshotAsync();
         //return screenshot.Data;
      }
      catch(Exception ex)
      {
         logger.LogError(ex, "Failed to create thumbnail for media {Id}", id);
         return null;
      }
   }
}