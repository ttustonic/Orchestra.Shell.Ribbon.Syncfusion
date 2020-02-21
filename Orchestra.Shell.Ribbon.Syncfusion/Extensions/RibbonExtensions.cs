// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RibbonExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra
{
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Services;
    using Syncfusion.Windows.Tools.Controls;
    using SyncfusionRibbonButton = Syncfusion.Windows.Tools.Controls.RibbonButton;

    public static class RibbonExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static void AddAboutButton(this Ribbon ribbon)
        {
            Argument.IsNotNull(() => ribbon);

            ribbon.AddRibbonButton(GetImageUri("/Resources/Images/about.png"), () =>
            {
                var aboutService = ServiceLocator.Default.ResolveType<IAboutService>();
                aboutService.ShowAboutAsync();
            });
        }

        public static SyncfusionRibbonButton AddRibbonButton(this Ribbon ribbon, ImageSource imageSource, Action action)
        {
            Argument.IsNotNull(() => ribbon);

            var button = AddRibbonButton(ribbon, action);

            if (imageSource != null)
            {
                button.SetCurrentValue(SyncfusionRibbonButton.SmallIconProperty, imageSource);
                button.SetCurrentValue(SyncfusionRibbonButton.LargeIconProperty, imageSource);
            }
            return button;
        }

        public static SyncfusionRibbonButton AddRibbonButton(this Ribbon ribbon, Uri imageUri, Action action)
        {
            Argument.IsNotNull(() => ribbon);

            return AddRibbonButton(ribbon, new BitmapImage(imageUri), action);
        }

        private static SyncfusionRibbonButton AddRibbonButton(this Ribbon ribbon, Action action)
        {
            Argument.IsNotNull(() => ribbon);

            Log.Debug("Adding button to ribbon");

            var ribbonButton = new SyncfusionRibbonButton();
            ribbonButton.SizeForm = Syncfusion.Windows.Tools.SizeForm.Small;

            if (action != null)
            {
                ribbonButton.Click += (sender, e) => action();
            }
/* (ToT)
            ribbon.ToolBarItems.Add(ribbonButton);
*/
            return ribbonButton;
        }

        private static Uri GetImageUri(string uri)
        {
            var finalUri = string.Format("pack://application:,,,/{0};component{1}", typeof(RibbonExtensions).Assembly.GetName().Name, uri);
            return new Uri(finalUri, UriKind.RelativeOrAbsolute);
        }
    }
}
