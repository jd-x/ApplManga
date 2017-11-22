using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.Utils.Base;
using jdx.ApplManga.Utils.Extensions;
using System.Windows.Controls;

namespace jdx.ApplManga.ViewModels {
    public class DialogWindowViewModel : WindowViewModel {
        #region Public properties

        public string DialogTitle { get; set; }

        public Control DialogContent { get; set; } 

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public DialogWindowViewModel(Window window) : base(window) {
            MinimumWindowWidth = 200;
            MinimumWindowHeight = 100;
        }
    }
}
