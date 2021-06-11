// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationInitializationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. 2021 Tomislav Tustonic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orchestra.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Threading;
    using Orc.Theming;
    using Orchestra.Theming;
    using Syncfusion.SfSkinManager;

    public class ApplicationInitializationServiceBase: IApplicationInitializationService
    {
        public virtual bool ShowSplashScreen => true;

        public virtual bool ShowShell => true;

        public virtual Task InitializeBeforeShowingSplashScreenAsync()
        {
            InitializeLogging();

            SyncThemes();

            return Task.CompletedTask;
        }

        public virtual Task InitializeBeforeCreatingShellAsync()
        {
            return Task.CompletedTask;
        }

        void SyncThemes()
        {
            var serviceLocator = this.GetServiceLocator();
            var xamlResourceService = serviceLocator.ResolveType<IXamlResourceService>();
            var themeService = serviceLocator.ResolveType<IThemeService>();
            var orchestraThemeManager = serviceLocator.ResolveType<IThemeManager>();
            var orcThemingThemeManager = serviceLocator.ResolveType<Orc.Theming.ThemeManager>();

            // Note: we only have to create style forwarders once
            var xamlResourceDictionaries = xamlResourceService.GetApplicationResourceDictionaries();

            foreach (var xamlResourceDictionary in xamlResourceDictionaries)
            {
                orchestraThemeManager.EnsureApplicationThemes(xamlResourceDictionary, false);
            }
            if (themeService.ShouldCreateStyleForwarders())
            {
                StyleHelper.CreateStyleForwardersForDefaultStyles();
            }

            orcThemingThemeManager.SynchronizeTheme();
        }

        public virtual Task InitializeAfterCreatingShellAsync()
        {
            var mainWin = Application.Current.MainWindow;
            Style style = Application.Current.TryFindResource("SyncfusionRibbonWindowStyle") as Style;
            if (style != null)
                mainWin.Style = style;  // set style explicitly to prevent delay in applying theme
            return Task.CompletedTask;
        }

        public virtual Task InitializeBeforeShowingShellAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task InitializeAfterShowingShellAsync()
        {
            return Task.CompletedTask;
        }

        protected static async Task RunAndWaitAsync(params Func<Task>[] actions)
        {
            await TaskHelper.RunAndWaitAsync(actions);
        }

        protected virtual void InitializeLogging()
        {
            LogHelper.CleanUpAllLogTypeFiles();

            var fileLogListener = LogHelper.CreateFileLogListener(LogFilePrefixes.EntryAssemblyName);

            fileLogListener.IsDebugEnabled = false;
            fileLogListener.IsInfoEnabled = true;
            fileLogListener.IsWarningEnabled = true;
            fileLogListener.IsErrorEnabled = true;

            LogManager.AddListener(fileLogListener);
        }
    }
}
