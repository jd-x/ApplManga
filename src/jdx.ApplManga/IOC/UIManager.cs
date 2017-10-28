using System;
using System.Threading.Tasks;
using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.Core.ViewModels;
using System.Windows.Forms;

namespace jdx.ApplManga.IOC {
    /// <summary>
    /// <see cref="IUIManager"/> implemention for this app
    /// </summary>
    public class UIManager : IUIManager {
        /// <summary>
        /// Displays a message dialog instance
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Task ShowMessageDialog(MsgBoxDialogViewModel viewModel) {
            return Task.Run(() => MessageBox.Show("Test"));
        }
    }
}
