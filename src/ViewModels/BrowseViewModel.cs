using ApplManga.ViewModels;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using ApplManga.Utils;
using System.Collections.Generic;

namespace ApplManga.ViewModels {
    public class BrowseViewModel : ViewModelBase {
        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        private ObservableCollection<MangaData> _mangaList = new ObservableCollection<MangaData>();
        public ObservableCollection<MangaData> MangaList {
            get { return _mangaList; }
            set { _mangaList = value; }
        }
        
        public BrowseViewModel(string tabCaption, string tabIcon) {
            this.TabCaption = tabCaption;
            this.TabIcon = tabIcon;

            WebScraper webScraper = new WebScraper();
            List<string> mangaTitles = new List<string>();

            mangaTitles = webScraper.GetCellsFromHTML(
                "https://www.mangaupdates.com/series.html?page=1&perpage=100&output=xmlp",
                "//table[contains(@class, 'series_rows_table')]",
                "tr[3]",
                "//td[contains(@class, 'col1')]/a/@href");

            foreach(string title in mangaTitles) {
                _mangaList.Add(new MangaData(title));
            }
        }
    }
}
