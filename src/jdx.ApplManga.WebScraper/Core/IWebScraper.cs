using jdx.ApplManga.WebScraper.Core.Repos;

namespace jdx.ApplManga.WebScraper.Core {
    public interface IWebScraper {
        void Scrape(IHtmlDocLoader htmlLoader, IWebScraperRepo webScraperContext);
    }
}
