using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using jdx.ApplManga.Core.DAL;
using jdx.ApplManga.Core.Models;

namespace jdx.ApplManga.WebScraper.Core.Repos {
    internal class WebScraperDataContext : DbContext {
        public WebScraperDataContext(string connectionString) : base(new SQLiteConnection() { ConnectionString = connectionString }, true) {
        }

        public DbSet<MangaList> Manga { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class WebScraperRepo : IWebScraperRepo {
        private readonly WebScraperDataContext _webScraperDataContext = new WebScraperDataContext(new DbManager().ConnectionString);

        public Database ScraperDb {
            get {
                return _webScraperDataContext.Database;
            }
        }

        public void AddEntry(MangaList record) {
            bool titleExists = _webScraperDataContext.Manga.Any(t => t.Title.Equals(record.Title));

            if (!titleExists) {
                _webScraperDataContext.Manga.Add(record);
            }
        }

        public IEnumerable<MangaList> GetEntireList() {
            return _webScraperDataContext.Manga.ToList();
        }

        public IEnumerable<MangaList> GetListBySite(string site) {
            return _webScraperDataContext.Manga.Where(m => m.Site.Equals(site));
        }

        public void RemoveEntry(MangaList record) {
            _webScraperDataContext.Manga.Remove(record);
        }

        public void SaveChanges() {
            _webScraperDataContext.SaveChanges();
        }
    }
}