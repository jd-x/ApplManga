using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.Controls.DialogEx {
    public class DialogMessageBoxDesignModel : MsgBoxDialogViewModel {
        public static DialogMessageBoxDesignModel Instance => new DialogMessageBoxDesignModel();

        public DialogMessageBoxDesignModel() {
            DialogText = "Design-time text";
            DialogOkCaption = "Dismiss";
        }
    }
}
