using System;
using System.Windows;

namespace jdx.ApplManga.Utils.Base {
    public static class Constants {
        // Shell ---------------------------------------------------------------------------------------------------------------
        public static readonly double WindowMinimumWidth = 1024;
        public static readonly double WindowMinimumHeight = 600;
        public static readonly double WindowTitleBarHeight = SystemParameters.WindowCaptionHeight + SystemParameters.ResizeFrameHorizontalBorderHeight;
        public static readonly double WindowResizeBorderSize = SystemParameters.ResizeFrameHorizontalBorderHeight;
        public static readonly int WindowOuterMarginSize = 10;

        // Animation -----------------------------------------------------------------------------------------------------------
        public static readonly Duration MouseEnterDuration = new Duration(TimeSpan.FromMilliseconds(50));
        public static readonly Duration MouseLeaveDuration = new Duration(TimeSpan.FromMilliseconds(250));
    }
}
