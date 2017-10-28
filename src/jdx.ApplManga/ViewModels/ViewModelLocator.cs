using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.ViewModels {
    /// <summary>
    /// Locates ViewModels from IoC for use in XAML binding
    /// </summary>
    public class ViewModelLocator {

        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        public static AppViewModel ApplicationViewModel => IoC.Get<AppViewModel>();
    }
}
