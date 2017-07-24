using System.Collections.Generic;
using System.Linq;
using ApplManga.Utils.WebScraper.Core.Repos;
using HtmlAgilityPack;

namespace ApplManga.Utils.WebScraper.Core.Scrapers {
    public class MangaFoxWebScraper : WebScraperBase, IWebScraper {
        private string _baseURL = @"http://mangafox.me";
        private string _searchBaseURL = @"http://mangafox.me/manga/";

        protected override string BaseURL {
            get { return _baseURL; }
        }

        protected override string SearchBaseURL {
            get { return _searchBaseURL; }
        }

        public void Scrape(IHtmlDocLoader htmlLoader, IWebScraperRepo webScraperContext) {
            HtmlLoader = htmlLoader;
            ScraperRepo = webScraperContext;

            StartScraping();
        }

        protected override string CreateNextURL(int nextPage) {
            return SearchBaseURL;
        }

        protected override IEnumerable<HtmlNode> GetMangaRows(HtmlDocument htmlDoc) {
            return htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'manga_list')]/ul//li");
        }

        protected override string GetMangaTitle(HtmlNode row) {
            return HtmlEntity.DeEntitize(row.Descendants("a")
                .Where(r => r.Attributes.Contains("class") && r.Attributes["class"].Value.Contains("series_preview"))
                .Select(r => r.InnerText).SingleOrDefault());
        }

        protected override string GetMangaURL(HtmlNode row) {
            return row.Descendants("a")
                .Where(r => r.Attributes.Contains("class") && r.Attributes["class"].Value.Contains("series_preview"))
                .Select(r => r.Attributes["href"].Value).SingleOrDefault();
        }

        protected override string GetMangaImagePath(HtmlNode row) {
            return "";
        }

        protected override string GetMangaPublishingStatus(HtmlNode row) {
            /*var pubStatus = row.Descendants("a")
                .Where(r => r.Attributes.Contains("class") && r.Attributes["class"].Value.Contains("series_preview"))
                .Select(r => r.Attributes["manga_open"].Name).SingleOrDefault();
            if (pubStatus == null) {
                return "Finished";
            } else {
                return "Ongoing";
            }*/
            return "Ongoing";
        }

        protected override bool HasOnePageOnly() {
            return true;
        }
    }
}
