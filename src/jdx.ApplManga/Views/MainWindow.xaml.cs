using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using jdx.ApplManga.ViewModels;

namespace jdx.ApplManga {
    [StructLayout(LayoutKind.Sequential)]
    internal struct Margins {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    internal enum AccentState {
        ACCENT_DISABLED = 1,
        ACCENT_ENABLE_GRADIENT = 0,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute {
        WCA_ACCENT_POLICY = 19
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        [DllImport("dwmapi.dll")]
        internal static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins margins);

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        internal void EnableBlur() {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
                AccentFlags = (0x20 | 0x40 | 0x80 | 0x100)
            };

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            // Removes the 1px window border
            var windowBorders = new Margins {
                Left = 0,
                Top = 0,
                Right = 0,
                Bottom = 0
            };

            DwmExtendFrameIntoClientArea(windowHelper.Handle, ref windowBorders);

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            EnableBlur();
        }

        public MainWindow() {
            InitializeComponent();
            DataContext = new MainViewModel(this);
            Loaded += MainWindow_Loaded;
        }

        private void ApplWindow_Activated(object sender, EventArgs e) {
            // Hide overlay if window is active
            (DataContext as MainViewModel).DimOverlayVisible = false;
        }

        private void ApplWindow_Deactivated(object sender, EventArgs e) {
            // Show overlay on lost focus
            (DataContext as MainViewModel).DimOverlayVisible = true;
        }
    }
}
