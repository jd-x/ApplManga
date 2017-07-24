using HtmlAgilityPack;

namespace ApplManga.Utils.WebScraper.Core {
    public interface IHtmlDocLoader {
        HtmlDocument LoadDocument(string url);
    }
}
