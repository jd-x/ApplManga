using System.Collections.Generic;
using System.Data.Entity;
using ApplManga.Models;

namespace ApplManga.Utils.WebScraper.Core.Repos {
    public interface IWebScraperRepo {
        Database ScraperDb { get; }

        void AddEntry(MangaList record);

        void RemoveEntry(MangaList record);

        IEnumerable<MangaList> GetListBySite(string site);
        IEnumerable<MangaList> GetEntireList();

        void SaveChanges();
    }
}
