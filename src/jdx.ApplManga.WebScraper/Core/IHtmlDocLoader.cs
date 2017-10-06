using HtmlAgilityPack;

namespace jdx.ApplManga.WebScraper.Core {
    public interface IHtmlDocLoader {
        HtmlDocument LoadDocument(string url);
    }
}
