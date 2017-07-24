using ApplManga.Utils.WebScraper.Core.Repos;

namespace ApplManga.Utils.WebScraper.Core {
    public interface IWebScraper {
        void Scrape(IHtmlDocLoader htmlLoader, IWebScraperRepo webScraperContext);
    }
}
