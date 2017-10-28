using System.Threading.Tasks;
using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.Core.IOC {
    /// <summary>
    /// UI manager for handling any UI interaction
    /// </summary>
    public interface IUIManager {
        /// <summary>
        /// Displays a message dialog instance
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task ShowMessageDialog(MsgBoxDialogViewModel viewModel);
    }
}
