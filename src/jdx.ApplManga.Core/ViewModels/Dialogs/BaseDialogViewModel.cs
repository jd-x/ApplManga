using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace jdx.ApplManga.Core.ViewModels {
    /// <summary>
    /// A base ViewModel for dialog box
    /// </summary>
    public class BaseDialogViewModel : BaseViewModel {
        public string DialogTitle { get; set; }
    }
}
