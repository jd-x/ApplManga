using System;
using System.Collections.Generic;
using System.IO;
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
            // Returns idx_# for testing
            return htmlDoc.DocumentNode.SelectNodes("//ul[contains(@id, 'idx_#')]//li");

            // Returns the entire manga list
            //return htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'manga_list')]/ul//li");
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

        protected override string GetMangaAuthor(HtmlNode row) {
            return "JADE LAUSA";
        }

        protected override string GetMangaImagePath(HtmlNode row) {
            var coverImageURLs = new List<string>();

            for (int i = 0; i < 40; i++) {
                coverImageURLs.Add("medium(" + i + ").jpg");
            }

            string rndCoverImage = coverImageURLs[new Random().Next(coverImageURLs.Count)];

            return Path.Combine("C:/Users/jadem/Desktop/UI Mockups/Sample Covers/", rndCoverImage);
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
