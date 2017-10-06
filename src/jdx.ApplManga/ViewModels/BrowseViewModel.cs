using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using jdx.ApplManga.AppLogger.Core.Logger;
using jdx.ApplManga.Core.Models;
using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.Utils.Extensions;
using jdx.ApplManga.WebScraper.Core;
using jdx.ApplManga.WebScraper.Core.Repos;
using jdx.ApplManga.WebScraper.Core.Scrapers;

namespace jdx.ApplManga.ViewModels {
    public class BrowseViewModel : ViewModelBase, IPageViewModel {
        public delegate void SelectionChange(string title, string author, string imagePath);

        public SelectionChange OnSelectionChange { get; set; }

        public string Name {
            get { return "BROWSE"; }
        }

        private CheckedListBoxItem<MangaList> _selectedItem;
        public CheckedListBoxItem<MangaList> SelectedItem {
            get { return _selectedItem; }
            set {
                if (_selectedItem != value) {
                    _selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        private string _selectedTitle;
        public string SelectedTitle {
            get { return _selectedTitle; }
            set {
                _selectedTitle = value;
                RaisePropertyChanged("SelectedTitle");

                OnSelectionChange?.Invoke(_selectedTitle, _selectedAuthor, _selectedImage);
            }
        }

        private string _selectedAuthor;
        public string SelectedAuthor {
            get { return _selectedAuthor; }
            set {
                _selectedAuthor = value;
                RaisePropertyChanged("SelectedAuthor");

                OnSelectionChange?.Invoke(_selectedTitle, _selectedAuthor, _selectedImage);
            }
        }

        private string _selectedImage;
        public string SelectedImage {
            get { return _selectedImage; }
            set {
                _selectedImage = value;
                RaisePropertyChanged("SelectedImage");

                OnSelectionChange?.Invoke(_selectedTitle, _selectedAuthor, _selectedImage);
            }
        }

        public ICommand ButtonCommand { get; set; }

        private CheckedObservableCollection<MangaList> _manga;
        public CheckedObservableCollection<MangaList> Manga {
            get { return _manga; }
            set {
                _manga = value;
                RaisePropertyChanged("Manga");
            }
        }

        internal CollectionViewSource MangaCVS { get; set; }

        private ICollectionView _mangaCV;
        public ICollectionView MangaCV {
            get {
                return _mangaCV;
            }
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

        protected string[] SortOptions = new string[] {
            "A to Z",
            "Release year",
            "Author"
        };

        protected string[] StatusOptions = new string[] {
            "All",
            "Completed",
            "Ongoing"
        };

        // Populate this with sites from database,
        // use default values if none is provided
        public string[] SiteOptions = new string[] {
            "All",
            "Bato.to",
            "KissManga",
            "MangaFox"
        };

        private string _sortOptionsSelectedValue;
        public string SortOptionsSelectedValue {
            get { return _sortOptionsSelectedValue; }
            set {
                _sortOptionsSelectedValue = value;
                RaisePropertyChanged("SortOptionsSelectedValue");
            }
        }

        private string _statusOptionsSelectedValue;
        public string StatusOptionsSelectedValue {
            get { return _statusOptionsSelectedValue; }
            set {
                _statusOptionsSelectedValue = value;
                RaisePropertyChanged("StatusOptionsSelectedValue");
            }
        }

        private string _siteOptionsSelectedValue;
        public string SiteOptionsSelectedValue {
            get { return _siteOptionsSelectedValue; }
            set {
                _siteOptionsSelectedValue = value;
                RaisePropertyChanged("SiteOptionsSelectedValue");
            }
        }

        private ObservableCollection<string> _sortComboOptions;
        public ObservableCollection<string> SortComboOptions {
            get { return _sortComboOptions; }
            set {
                if (_sortComboOptions != value) {
                    _sortComboOptions = value;
                    RaisePropertyChanged("SortComboOptions");
                }
            }
        }

        private ObservableCollection<string> _statusComboOptions;
        public ObservableCollection<string> StatusComboOptions {
            get { return _statusComboOptions; }
            set {
                if (_statusComboOptions != value) {
                    _statusComboOptions = value;
                    RaisePropertyChanged("StatusComboOptions");
                }
            }
        }

        private ObservableCollection<string> _siteComboOptions;
        public ObservableCollection<string> SiteComboOptions {
            get { return _siteComboOptions; }
            set {
                if (_siteComboOptions != value) {
                    _siteComboOptions = value;
                    RaisePropertyChanged("SiteComboOptions");
                }
            }
        }

        /*private void ButtonClick() {
            var checkedItems = Manga.Where(item => item.IsChecked == true);

            foreach (var obj in checkedItems) {
                AppLogHelper.Log(AppLoggerBase.LogTarget.File, string.Concat(obj.Item.Title, " :: ", obj.Item.Site, " :: ", obj.Item.PubStatus));
                Console.WriteLine(string.Concat(obj.Item.Title, " :: ", obj.Item.Site));
            }
        }*/

        public BrowseViewModel() {
            //ButtonCommand = new RelayCommand(obj => ButtonClick());

            // Populate sort dropdown values
            SortComboOptions = new ObservableCollection<string>(SortOptions);
            StatusComboOptions = new ObservableCollection<string>(StatusOptions);
            SiteComboOptions = new ObservableCollection<string>(SiteOptions);

            // Set default selected options
            SortOptionsSelectedValue = SortOptions[0];
            StatusOptionsSelectedValue = StatusOptions[0];
            SiteOptionsSelectedValue = SiteOptions[0];

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

                _mangaCV = CollectionViewSource.GetDefaultView(MangaCVS.View);
                _mangaCV.CurrentChanged += delegate {
                    _selectedItem = (CheckedListBoxItem<MangaList>)_mangaCV.CurrentItem;

                    if (SelectedItem != null) {
                        SelectedTitle = SelectedItem.Item.Title;
                        SelectedAuthor = SelectedItem.Item.Author;
                        SelectedImage = SelectedItem.Item.ImagePath;
                    }

                    Console.WriteLine(SelectedTitle + " | " + SelectedImage);
                };

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
