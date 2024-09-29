#if WINDOWS
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Interop;
using WinRT.Interop; // Required for window interop
#endif


namespace MauiNestedNotify
{
    public partial class AppShell : Shell
    {
        public AppShell() => InitializeComponent();
    }
}
