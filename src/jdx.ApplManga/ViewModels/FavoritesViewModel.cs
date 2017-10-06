﻿using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.ViewModels {
    public class FavoritesViewModel : ViewModelBase, IPageViewModel {
        public string Name {
            get { return "Favorites"; }
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        public FavoritesViewModel(string tabIcon) {
            TabCaption = Name;
            TabIcon = tabIcon;
        }
    }
}