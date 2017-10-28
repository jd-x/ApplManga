using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jdx.ApplManga.Core.ViewModels {
    public class MsgBoxDialogViewModel : BaseViewModel {
        #region Public properties

        public string MsgTitle { get; set; }

        public string MsgContent { get; set; }

        public string MsgOkCaption { get; set; }

        #endregion
    }
}
