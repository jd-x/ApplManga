using System;
using System.Windows;

namespace ApplManga.Utils.Base {
    public static class Constants {
        // Shell
        public static readonly int WindowBorderMargin = 7;

        // Animation
        public static readonly Duration MouseEnterDuration = new Duration(TimeSpan.FromMilliseconds(50));
        public static readonly Duration MouseLeaveDuration = new Duration(TimeSpan.FromMilliseconds(250));
    }
}
