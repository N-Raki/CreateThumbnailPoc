using CefSharp.OffScreen;

namespace CreateThumbnail;

internal sealed class CefSharpHelper : ICefSharpHelper, IDisposable
{
    private ChromiumWebBrowser? _browser;
    public ChromiumWebBrowser Browser
    {
        get
        {
            _browser ??= new ChromiumWebBrowser();
            return _browser!;
        }
    }

    public void Dispose()
    {
        _browser?.Dispose();
    }
}