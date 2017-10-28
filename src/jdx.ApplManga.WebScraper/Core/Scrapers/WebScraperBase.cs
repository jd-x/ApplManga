using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using jdx.ApplManga.AppLogger.Core.Logger;
using jdx.ApplManga.Core.Models;
using jdx.ApplManga.WebScraper.Core.Repos;

namespace jdx.ApplManga.WebScraper.Core.Scrapers {
    public abstract class WebScraperBase {
        protected abstract string BaseURL { get; }
        protected abstract string SearchBaseURL { get; }

        protected IHtmlDocLoader HtmlLoader { get; set; }
        protected IWebScraperRepo ScraperRepo { get; set; }

        protected abstract IEnumerable<HtmlNode> GetMangaRows(HtmlDocument htmlDoc);
        protected abstract bool HasOnePageOnly();
        protected abstract string CreateNextURL(int nextPage);
        protected abstract string GetMangaTitle(HtmlNode row);
        protected abstract string GetMangaURL(HtmlNode row);
        protected abstract string GetMangaAuthor(HtmlNode row);
        protected abstract string GetMangaImagePath(HtmlNode row);
        protected abstract string GetMangaPublishingStatus(HtmlNode row);

        protected virtual void StartScraping() {
            AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Started WebScraper @(" + BaseURL + ")...");

            for (var nextPage = 1; ; nextPage++) {
                var nextURL = CreateNextURL(nextPage);
                var doc = HtmlLoader.LoadDocument(nextURL);

                AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Processing page [" + nextPage.ToString() + "] @(" + nextURL + ")");

                var rows = GetMangaRows(doc);
                var rowCount = rows.Count();

                AppLogHelper.Log(AppLoggerBase.LogTarget.File, "[" + rowCount + "] rows found. Processing rows...");

                if (rowCount == 0) {
                    AppLogHelper.Log(AppLoggerBase.LogTarget.File, "No more titles found, exiting main loop...");
                    break;
                }

                foreach (var row in rows) {
                    var title = GetMangaTitle(row);
                    if (title == null) {
                        AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Failed in extracting title, skipping...");
                        continue;
                    }

                    var titleURL = GetMangaURL(row);
                    if (titleURL == null) {
                        AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Failed in extracting URL, skipping...");
                        continue;
                    }

                    var author = GetMangaAuthor(row);
                    if (author == null) {
                        AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Failed in extracting author, skipping...");
                        continue;
                    }

                    var imagePath = GetMangaImagePath(row);
                    if (imagePath == null) {
                        AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Failed in extracting image URL, skipping...");
                        continue;
                    }

                    var pubStatus = GetMangaPublishingStatus(row);
                    if (pubStatus == null) {
                        AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Failed in extracting publishing status, skipping...");
                        continue;
                    }

                    AppLogHelper.Log(AppLoggerBase.LogTarget.File, title + ", " + titleURL);

                    var mangaEntry = new MangaList {
                        Title = title,
                        Site = titleURL,
                        Author = author,
                        ImagePath = imagePath,
                        PubStatus = pubStatus
                    };

                    ScraperRepo.AddEntry(mangaEntry);
                }
                ScraperRepo.SaveChanges();
                AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Successfully added [" + rowCount + "] records to repository");

                var hasOnePageOnly = HasOnePageOnly();
                if (hasOnePageOnly) {
                    break;
                }

                AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Finished scraping page [" + nextPage + "]");
            }
            AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Finished scraping @(" + BaseURL + ")");
        }
    }
}
