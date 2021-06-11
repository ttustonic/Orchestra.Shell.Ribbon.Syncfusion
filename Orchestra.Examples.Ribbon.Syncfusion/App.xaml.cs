﻿namespace Orchestra.Examples.Ribbon
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Services;
    using Orchestra.Services;
    using Orchestra.Views;
    using Syncfusion.Licensing;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly Stopwatch _stopwatch;
        #endregion

        #region Constructors
        public App()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            SyncfusionLicenseProvider.RegisterLicense("NDI4ODk0QDMxMzkyZTMxMmUzMGZucmh5dGF3eUZQekR0d1I5N3RhcWpueWFZVHY3aEhPUWZLQ3liRFdTSDg9");
        }
        #endregion

        #region Methods
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener(true);
#endif

            var languageService = ServiceLocator.Default.ResolveType<ILanguageService>();

            // Note: it's best to use .CurrentUICulture in actual apps since it will use the preferred language
            // of the user. But in order to demo multilingual features for devs (who mostly have en-US as .CurrentUICulture),
            // we use .CurrentCulture for the sake of the demo
            languageService.PreferredCulture = CultureInfo.CurrentCulture;
            languageService.FallbackCulture = new CultureInfo("en-US");

            Orc.Theming.FontImage.RegisterFont("FontAwesome",
                new FontFamily(new Uri("pack://application:,,,/Orchestra.Examples.Ribbon.Syncfusion;component/Resources/Fonts/", UriKind.RelativeOrAbsolute),
                "./#FontAwesome"));
            Orc.Theming.FontImage.DefaultFontFamily = "FontAwesome";

            var serviceLocator = ServiceLocator.Default;
            var shellService = serviceLocator.ResolveType<IShellService>();
            shellService.CreateAsync<ShellWindow>();

            _stopwatch.Stop();

            Log.Info("Elapsed startup stopwatch time: {0}", _stopwatch.Elapsed);
        }
        #endregion
    }
}
