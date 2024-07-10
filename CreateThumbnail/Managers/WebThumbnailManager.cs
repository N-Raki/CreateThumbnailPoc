using System.Drawing;
using CefSharp;

namespace CreateThumbnail.Managers;

internal sealed class WebThumbnailManager : IWebThumbnailManager
{
    private readonly ICefSharpHelper _cefSharpHelper;
    
    public WebThumbnailManager(ICefSharpHelper cefSharpHelper)
    {
        _cefSharpHelper = cefSharpHelper;
    }
    
    public async Task<byte[]?> CreateWebThumbnailAsync(int id)
    {
        await _cefSharpHelper.Browser.LoadUrlAsync("https://google.com");
        await _cefSharpHelper.Browser.WaitForInitialLoadAsync();
        using var devToolsClient = _cefSharpHelper.Browser.GetDevToolsClient();
        // screenshot with page
        var screenshot = await devToolsClient.Page.CaptureScreenshotAsync();
        return screenshot.Data;
    }
}