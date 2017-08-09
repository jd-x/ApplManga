using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using ApplManga.Models;
using ApplManga.Utils.AppLogger.Core.Logger;
using ApplManga.Utils.Extensions;
using ApplManga.Utils.WebScraper.Core;
using ApplManga.Utils.WebScraper.Core.Repos;
using ApplManga.Utils.WebScraper.Core.Scrapers;

namespace ApplManga.ViewModels {
    public class BrowseViewModel : ViewModelBase {
        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        public ICommand ButtonCommand { get; set; }

        private CheckedObservableCollection<MangaList> _manga;
        public CheckedObservableCollection<MangaList> Manga {
            get { return _manga; }
            set {
                _manga = value;
                OnPropertyChanged("Manga");
            }
        }

        internal CollectionViewSource MangaCVS { get; set; }
        public ICollectionView MangaCV {
            get { return MangaCVS.View; }
        }

        private string _filter;
        public string Filter {
            get { return _filter; }
            set {
                _filter = value;
                OnFilterChanged();
            }
        }

        private void OnFilterChanged() {
            MangaCVS.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e) {
            CheckedListBoxItem<MangaList> mangaVM = (CheckedListBoxItem<MangaList>)e.Item;

            if (string.IsNullOrWhiteSpace(_filter) || _filter.Length == 0) {
                e.Accepted = true;
            } else {
                e.Accepted = mangaVM.Item.Title.Contains(Filter);
            }
        }

        private void ButtonClick() {
            var checkedItems = Manga.Where(item => item.IsChecked == true);

            foreach (var obj in checkedItems) {
                AppLogHelper.Log(AppLoggerBase.LogTarget.File, string.Concat(obj.Item.Title," :: ",obj.Item.Site," :: ",obj.Item.PubStatus));
            }
        }

        public BrowseViewModel(string tabCaption, string tabIcon) {
            TabCaption = tabCaption;
            TabIcon = tabIcon;

            // Add button click functionality
            ButtonCommand = new RelayCommand(obj => ButtonClick());

            try {
                var htmlLoader = new HtmlDocLoader();
                var scraperRepo = new WebScraperRepo();
                var scrapers = new IWebScraper[] { new MangaFoxWebScraper() };

                /*foreach (var scraper in scrapers) {
                    scraper.Scrape(htmlLoader, scraperRepo);
                }*/

                Manga = new CheckedObservableCollection<MangaList>();
                //scraperRepo.GetEntireList().Distinct().ToList().ForEach(i => Manga.Add(i));
                Manga.AddRange(scraperRepo.GetEntireList().ToList());

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
