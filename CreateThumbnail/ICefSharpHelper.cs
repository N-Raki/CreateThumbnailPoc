using CefSharp.OffScreen;

namespace CreateThumbnail;

public interface ICefSharpHelper
{
    ChromiumWebBrowser Browser { get; }
}