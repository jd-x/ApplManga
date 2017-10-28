using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using jdx.ApplManga.Core.Models;
using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.Utils.Extensions;
using jdx.ApplManga.WebScraper.Core;
using jdx.ApplManga.WebScraper.Core.Repos;
using jdx.ApplManga.WebScraper.Core.Scrapers;
using jdx.ApplManga.Core.IOC;
using System.Threading.Tasks;

namespace jdx.ApplManga.ViewModels {
    public class BrowseViewModel : BaseViewModel {
        public string Name {
            get { return "BROWSE"; }
        }

        public string TabIcon { get; private set; }

        #region ListView properties

        public delegate void SelectionChange(string title, string author, string imagePath);

        public SelectionChange OnSelectionChange { get; set; }

        private CheckedListBoxItem<MangaList> _selectedItem;
        public CheckedListBoxItem<MangaList> SelectedItem {
            get { return _selectedItem; }
            set {
                if (_selectedItem != value) {
                    _selectedItem = value;
                    RaisePropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private string _selectedTitle;
        public string SelectedTitle {
            get { return _selectedTitle; }
            set {
                _selectedTitle = value;
                RaisePropertyChanged(nameof(SelectedTitle));

                OnSelectionChange?.Invoke(_selectedTitle, _selectedAuthor, _selectedImage);
            }
        }

        private string _selectedAuthor;
        public string SelectedAuthor {
            get { return _selectedAuthor; }
            set {
                _selectedAuthor = value;
                RaisePropertyChanged(nameof(SelectedAuthor));

                OnSelectionChange?.Invoke(_selectedTitle, _selectedAuthor, _selectedImage);
            }
        }

        private string _selectedImage;
        public string SelectedImage {
            get { return _selectedImage; }
            set {
                _selectedImage = value;
                RaisePropertyChanged(nameof(SelectedImage));

                OnSelectionChange?.Invoke(_selectedTitle, _selectedAuthor, _selectedImage);
            }
        }

        private CheckedObservableCollection<MangaList> _manga;
        public CheckedObservableCollection<MangaList> Manga {
            get { return _manga; }
            set {
                _manga = value;
                RaisePropertyChanged(nameof(Manga));
            }
        }

        internal CollectionViewSource MangaCVS { get; set; }

        private ICollectionView _mangaCV;
        public ICollectionView MangaCV => _mangaCV;

        #endregion

        #region SearchBox properties

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

        #endregion

        //#region ComboBox properties

        //protected string[] SortOptions = new string[] {
        //    "A to Z",
        //    "Release year",
        //    "Author"
        //};

        //protected string[] StatusOptions = new string[] {
        //    "All",
        //    "Completed",
        //    "Ongoing"
        //};

        //// Populate this with sites from database,
        //// use default values if none is provided
        //public string[] SiteOptions = new string[] {
        //    "All",
        //    "Bato.to",
        //    "KissManga",
        //    "MangaFox"
        //};

        //private string _sortOptionsSelectedValue;
        //public string SortOptionsSelectedValue {
        //    get { return _sortOptionsSelectedValue; }
        //    set {
        //        _sortOptionsSelectedValue = value;
        //        RaisePropertyChanged(nameof(SortOptionsSelectedValue));
        //    }
        //}

        //private string _statusOptionsSelectedValue;
        //public string StatusOptionsSelectedValue {
        //    get { return _statusOptionsSelectedValue; }
        //    set {
        //        _statusOptionsSelectedValue = value;
        //        RaisePropertyChanged(nameof(StatusOptionsSelectedValue));
        //    }
        //}

        //private string _siteOptionsSelectedValue;
        //public string SiteOptionsSelectedValue {
        //    get { return _siteOptionsSelectedValue; }
        //    set {
        //        _siteOptionsSelectedValue = value;
        //        RaisePropertyChanged(nameof(SiteOptionsSelectedValue));
        //    }
        //}

        //private ObservableCollection<string> _sortComboOptions;
        //public ObservableCollection<string> SortComboOptions {
        //    get { return _sortComboOptions; }
        //    set {
        //        if (_sortComboOptions != value) {
        //            _sortComboOptions = value;
        //            RaisePropertyChanged(nameof(SortComboOptions));
        //        }
        //    }
        //}

        //private ObservableCollection<string> _statusComboOptions;
        //public ObservableCollection<string> StatusComboOptions {
        //    get { return _statusComboOptions; }
        //    set {
        //        if (_statusComboOptions != value) {
        //            _statusComboOptions = value;
        //            RaisePropertyChanged(nameof(StatusComboOptions));
        //        }
        //    }
        //}

        //private ObservableCollection<string> _siteComboOptions;
        //public ObservableCollection<string> SiteComboOptions {
        //    get { return _siteComboOptions; }
        //    set {
        //        if (_siteComboOptions != value) {
        //            _siteComboOptions = value;
        //            RaisePropertyChanged(nameof(SiteComboOptions));
        //        }
        //    }
        //}

        //private void SetupComboBoxes() {
        //    // Populate sort dropdown values
        //    SortComboOptions = new ObservableCollection<string>(SortOptions);
        //    StatusComboOptions = new ObservableCollection<string>(StatusOptions);
        //    SiteComboOptions = new ObservableCollection<string>(SiteOptions);

        //    // Set default selected options
        //    SortOptionsSelectedValue = SortOptions[0];
        //    StatusOptionsSelectedValue = StatusOptions[0];
        //    SiteOptionsSelectedValue = SiteOptions[0];
        //}

        //#endregion

        #region Commands

        /// <summary>
        /// Switches to the selected item's <see cref="InfoView"/>
        /// </summary>
        public ICommand SwitchToInfoCommand { get; set; }

        #endregion

        /// <summary>
        /// Passes the <see cref="SelectedItem"/> info to the InfoViewModel
        /// </summary>
        /// <param name="selectedItem">The selected manga title</param>
        /// <returns></returns>
        public async Task SwitchToInfoAsync(object selectedItem) {
            IoC.Get<AppViewModel>().SwitchToView(AppView.Info);

            Console.WriteLine("Switch to Info view command invoked");

            await Task.Delay(1);
        }

        public void ShowDialog() {
            IoC.UIManager.ShowMessageDialog(new MsgBoxDialogViewModel {
                MsgTitle = "This is a test dialog",
                MsgContent = "Sample message content",
                MsgOkCaption = "Dismiss"
            });
        }

        public BrowseViewModel() {
            // Initialize values for filtering and sorting
            // SetupComboBoxes();

            SwitchToInfoCommand = new RelayCommand(() => ShowDialog());
            //SwitchToInfoCommand = new RelayParamCommand(async (selectedItem) => await SwitchToInfoAsync(selectedItem));

            try {
                var htmlLoader = new HtmlDocLoader();
                var scraperRepo = new WebScraperRepo();
                var scrapers = new IWebScraper[] { new MangaFoxWebScraper() };

                // Uncomment this block to update manga list database
                /*foreach (var scraper in scrapers) {
                    scraper.Scrape(htmlLoader, scraperRepo);
                }*/

                Manga = new CheckedObservableCollection<MangaList>();

                //scraperRepo.GetEntireList().Distinct().ToList().ForEach(i => Manga.Add(i));
                Manga.AddRange(scraperRepo.GetEntireList().ToList());

                MangaCVS = new CollectionViewSource {
                    Source = Manga
                };
                MangaCVS.Filter += ApplyFilter;

                _mangaCV = CollectionViewSource.GetDefaultView(MangaCVS.View);
                _mangaCV.CurrentChanged += delegate {
                    _selectedItem = (CheckedListBoxItem<MangaList>)_mangaCV.CurrentItem;

                    if (SelectedItem != null) {
                        SelectedTitle = SelectedItem.Item.Title;
                        SelectedAuthor = SelectedItem.Item.Author;
                        SelectedImage = SelectedItem.Item.ImagePath;
                    }

                    // For debugging only
                    Console.WriteLine(SelectedTitle + " | " + SelectedImage);

                    //AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Selected Title: " + SelectedTitle + " <" + SelectedImage + ">");
                };
            } catch (Exception ex) {
                //AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Error running WebScraper: " + ex.Message);
                Console.WriteLine(ex.InnerException);
            }
        }
    }
}
