// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShellWindow.xaml.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra.Views
{
    using System.Windows;
    using Catel.IoC;
    using Catel.Windows;
    using Services;
    using Syncfusion.SfSkinManager;
    using Syncfusion.Windows.Tools.Controls;

    /// <summary>
    /// Interaction logic for ShellWindow.xaml.
    /// </summary>
    public partial class ShellWindow: IShell
    {
        IServiceLocator _serviceLocator;
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellWindow"/> class.
        /// </summary>
        public ShellWindow()
        {
            _serviceLocator = ServiceLocator.Default;
            InitializeComponent();
            SfSkinManager.SetTheme(this, new Theme(SyncfusionThemeConfig.ApplicationTheme));
            _serviceLocator.RegisterInstance(pleaseWaitProgressBar, "pleaseWaitService");

            var statusService = _serviceLocator.ResolveType<IStatusService>();
            statusService.Initialize(statusTextBlock);

            var dependencyResolver = this.GetDependencyResolver();
            var ribbonService = dependencyResolver.Resolve<IRibbonService>();

            var ribbonContent = ribbonService.GetRibbon();
            if (ribbonContent != null)
            {
                ribbonContentControl.SetCurrentValue(ContentProperty, ribbonContent);

                var ribbon = ribbonContent.FindVisualDescendantByType<Ribbon>();
                if (ribbon != null)
                {
                    _serviceLocator.RegisterInstance(ribbon);
                }
            }

            var statusBarContent = ribbonService.GetStatusBar();
            if (statusBarContent != null)
            {
                customStatusBarItem.SetCurrentValue(ContentProperty, statusBarContent);
            }

            var mainView = ribbonService.GetMainView();
            contentControl.Content = mainView;

            ShellDimensionsHelper.ApplyDimensions(this, mainView);
        }
    }
}
