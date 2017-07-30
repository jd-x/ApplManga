using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using ApplManga.Models;
using ApplManga.Utils.AppLogger.Core.Logger;
using ApplManga.Utils.WebScraper.Core;
using ApplManga.Utils.WebScraper.Core.Repos;
using ApplManga.Utils.WebScraper.Core.Scrapers;

namespace ApplManga.ViewModels {
    public class BrowseViewModel : ViewModelBase {
        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        private ObservableCollection<MangaList> _manga;
        public ObservableCollection<MangaList> Manga {
            get { return _manga; }
            set { _manga = value; }
        }

        internal CollectionViewSource MangaCVS { get; set; }
        public ICollectionView MangaCV {
            get { return MangaCVS.View; }
        }

        private string _filter;
        public string Filter {
            get { return this._filter; }
            set {
                this._filter = value;
                OnFilterChanged();
            }
        }

        private void OnFilterChanged() {
            MangaCVS.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e) {
            MangaList mangaVM = (MangaList)e.Item;

            if (string.IsNullOrWhiteSpace(_filter) || _filter.Length == 0) {
                e.Accepted = true;
            } else {
                e.Accepted = mangaVM.Title.Contains(Filter);
            }
        }

        public BrowseViewModel(string tabCaption, string tabIcon) {
            this.TabCaption = tabCaption;
            this.TabIcon = tabIcon;

            try {
                var htmlLoader = new HtmlDocLoader();
                var scraperRepo = new WebScraperRepo();
                var scrapers = new IWebScraper[] { new MangaFoxWebScraper() };

                /*foreach (var scraper in scrapers) {
                    scraper.Scrape(htmlLoader, scraperRepo);
                }*/

                _manga = new ObservableCollection<MangaList>(scraperRepo.GetEntireList());

                MangaCVS = new CollectionViewSource();
                MangaCVS.Source = Manga;
                MangaCVS.Filter += ApplyFilter;

                AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Sample log entry");
            } catch (Exception ex) {
                AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Error running WebScraper: " + ex.Message);
                Console.WriteLine(ex.InnerException);
            }


            /*MangaImgs = new ObservableCollection<MangaImages> { };
            DirectoryInfo mangaCoverDir = new DirectoryInfo(@"C:\Users\jadem\Desktop\ThumbnailSource");

            foreach (FileInfo mangaCoverImage in mangaCoverDir.GetFiles("*.jpg")) {
                MangaImgs.Add(new MangaImages { ImagePath = mangaCoverImage.FullName, Title = mangaCoverImage.Name });
            }

            _mangaImgs = new ObservableCollection<MangaImages>(MangaImgs);*/
        }

        /*private ObservableCollection<MangaImages> _mangaImgs;
        public ObservableCollection<MangaImages> MangaImgs {
            get { return _mangaImgs; }
            set { _mangaImgs = value; }
        }*/
    }

    /*public class MangaImages {
        public string ImagePath { get; set; }
        public string Title { get; set; }
    }*/
}
