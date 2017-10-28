using jdx.ApplManga.Core.ViewModels;
using Ninject;

namespace jdx.ApplManga.Core.IOC {
    public static class IoC {
        #region Public properties

        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static IUIManager UIManager => IoC.Get<IUIManager>();

        #endregion

        public static void Setup() {
            BindViewModels();
        }

        // Bind all singleton ViewModels
        private static void BindViewModels() {
            Kernel.Bind<AppViewModel>().ToConstant(new AppViewModel());

            // Bind to single instances of the specified ViewModels
            /*Kernel.Bind<BrowseViewModel>().ToConstant(new BrowseViewModel());*/
            Kernel.Bind<DownloadsViewModel>().ToConstant(new DownloadsViewModel());
            Kernel.Bind<InfoViewModel>().ToConstant(new InfoViewModel());
        }

        public static T Get<T>() { return Kernel.Get<T>(); }
    }
}
