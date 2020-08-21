namespace Orchestra.Themes
{
    using Catel;
    using Catel.Logging;
    using ControlzEx.Theming;
    using Orc.Theming;
    using Orchestra.Services;
    using Orchestra.Theming;
    using System;
    using System.Windows;

    public class SyncfusionRibbonShellTheme: IShellTheme
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IThemeService _themeService;

        public SyncfusionRibbonShellTheme(IAccentColorService accentColorService,
            IThemeService themeService, IBaseColorSchemeService baseColorSchemeService)
        {
            Argument.IsNotNull(() => accentColorService);
            Argument.IsNotNull(() => themeService);
            _themeService = themeService;
            accentColorService.AccentColorChanged += OnAccentBaseSchemeColorChanged;
            baseColorSchemeService.BaseColorSchemeChanged += OnAccentBaseSchemeColorChanged;
        }

        private void OnAccentBaseSchemeColorChanged(object sender, EventArgs e)
        {
            ApplyTheme(_themeService.GetThemeInfo());
        }

        public ResourceDictionary CreateResourceDictionary(ThemeInfo themeInfo)
        {
            // Not required
            return null;
        }

        public void ApplyTheme(ThemeInfo themeInfo)
        {            
            var application = Application.Current;
            var applicationResources = application.Resources;
//            var resourceDictionary = Orchestra.ThemeHelper.GetAccentColorResourceDictionary();
            SyncfusionRibbonThemeHelper.CreateTheme(themeInfo.BaseColorScheme, themeInfo.AccentBaseColor, themeInfo.HighlightColor, changeImmediately: true);

            //applicationResources.MergedDictionaries.Insert(1, resourceDictionary);
        }
    }
}
