using System.Windows;

namespace Orchestra
{
    public static class SyncfusionThemeConfig
    {
        internal static readonly SupportedTheme DefaultTheme = SupportedTheme.FluentLight;

        public static string ApplicationTheme { get; set; }

        public static string ApplicationThemeName => ApplicationTheme.ToString();

        static SyncfusionThemeConfig()
        {
            var res =
                (Application.Current.TryFindResource("SyncfusionThemeName") ?? DefaultTheme).ToString();
            ApplicationTheme = res;
        }
    }
}
