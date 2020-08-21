// Note: this code comes from the Fluent.Ribbon showcase example licensed MIT:
// https://raw.githubusercontent.com/fluentribbon/Fluent.Ribbon/develop/Fluent.Ribbon.Showcase/Helpers/ThemeHelper.cs

namespace Orchestra
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Xml;

    public static class SyncfusionRibbonThemeHelper
    {
        public static string GenerateThemeName(string baseColorScheme, Color accentBaseColor, Color highlightColor)
        {
            return $"RuntimeTheme_{baseColorScheme}_{accentBaseColor.ToString().Replace("#", string.Empty)}";
        }

        private static SolidColorBrush ToBrush(this Color color)
        {
            return new SolidColorBrush(color);
        }

        public static Tuple<string, ResourceDictionary> CreateTheme(string baseColorScheme, Color accentBaseColor, Color highlightColor, string name = null, bool changeImmediately = false)
        {
            name ??= GenerateThemeName(baseColorScheme, accentBaseColor, highlightColor);

            var pbck = Application.Current.Resources["PrimaryBackground"] ;
            
            var textColor = Colors.Black;
            var accentBaseBrush = accentBaseColor.ToBrush();
            var accentBrush80 = Color.FromArgb(204, accentBaseColor.R, accentBaseColor.G, accentBaseColor.B).ToBrush();
            var accentBrush60 = Color.FromArgb(153, accentBaseColor.R, accentBaseColor.G, accentBaseColor.B).ToBrush();
            var accentBrush40 = Color.FromArgb(102, accentBaseColor.R, accentBaseColor.G, accentBaseColor.B).ToBrush();
            var accentBrush20 = Color.FromArgb(51, accentBaseColor.R, accentBaseColor.G, accentBaseColor.B).ToBrush();

            var hover = GetHoverColor(accentBaseBrush);
            var pressed = GetPressedColor(accentBaseBrush);
            var selected = GetSelectedColor(accentBaseBrush);
            var bsSelected = GetBackStageSelectedColor(accentBaseBrush);

            Application.Current.Resources["ActiveBackgroundBrush"] = accentBaseBrush;
            Application.Current.Resources["AccentColorBrush"] = accentBaseBrush;
            Application.Current.Resources["ActiveDarkBackground"] = bsSelected;
            Application.Current.Resources["ActiveLightBackground"] = pressed;

            Application.Current.Resources["HoverBackgroundBrush"] = hover;
            Application.Current.Resources["HoverBorderBrush"] = accentBaseBrush ;

            Application.Current.Resources["ActiveForegroundBrush"] = IdealTextColor(accentBaseColor).ToBrush() ;
            //            Application.Current.Resources["HoverForegroundBrush"] = Colors.White.ToBrush();
            //            Application.Current.Resources["HoverGlyphBrush"] = Colors.Pink.ToBrush();

            Application.Current.Resources["PrimaryBackground"] = accentBaseBrush;
            Application.Current.Resources["PrimaryBackgroundOpacity"] = accentBrush20;
            Application.Current.Resources["PrimaryBackgroundOpacity2"] = accentBrush40;
            Application.Current.Resources["PrimaryBackgroundOpacity3"] = accentBrush60;


            return new Tuple<string, ResourceDictionary>(null, null);
        }

        static Brush GetHoverColor(Brush brush)
        {
            Color color = (brush as SolidColorBrush).Color;
            double num = 0.75;
            byte r = (byte)((double)(int)color.R + (double)(255 - color.R) * num);
            byte g = (byte)((double)(int)color.G + (double)(255 - color.G) * num);
            byte b = (byte)((double)(int)color.B + (double)(255 - color.B) * num);
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        static Brush GetBackStageSelectedColor(Brush brush)
        {
            Color color = (brush as SolidColorBrush).Color;
            byte r = (byte)(color.R + 20);
            byte g = (byte)(color.G + 20);
            byte b = (byte)(color.B + 20);
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        static Brush GetPressedColor(Brush brush)
        {
            Color color = (brush as SolidColorBrush).Color;
            double num = 0.6;
            byte r = (byte)((double)(int)color.R + (double)(255 - color.R) * num);
            byte g = (byte)((double)(int)color.G + (double)(255 - color.G) * num);
            byte b = (byte)((double)(int)color.B + (double)(255 - color.B) * num);
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        static Brush GetSelectedColor(Brush brush)
        {
            Color color = (brush as SolidColorBrush).Color;
            double num = 0.5;
            byte r = (byte)((double)(int)color.R + (double)(255 - color.R) * num);
            byte g = (byte)((double)(int)color.G + (double)(255 - color.G) * num);
            byte b = (byte)((double)(int)color.B + (double)(255 - color.B) * num);
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public static string GetResourceDictionaryContent(ResourceDictionary resourceDictionary)
        {
            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw, new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "    "
                }))
                {
                    XamlWriter.Save(resourceDictionary, writer);

                    return sw.ToString();
                }
            }
        }

        /// <summary>
        ///     Determining Ideal Text Color Based on Specified Background Color
        ///     http://www.codeproject.com/KB/GDI-plus/IdealTextColor.aspx
        /// </summary>
        /// <param name="color">The bg.</param>
        /// <returns></returns>
        private static Color IdealTextColor(Color color)
        {
            const int nThreshold = 105;
            var bgDelta = Convert.ToInt32((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114));
            var foreColor = 255 - bgDelta < nThreshold
                                ? Colors.Black
                                : Colors.White;
            return foreColor;
        }

        private static string GetThemeTemplateContent()
        {
            throw new NotImplementedException();
            /* (ToT)
                        using (var stream = typeof(ThemeManager).Assembly.GetManifestResourceStream("Fluent.Themes.Themes.Theme.Template.xaml"))
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                return reader.ReadToEnd();
                            }
                        }
            */
        }

        private static string GetGeneratorParametersJson()
        {
            throw new NotImplementedException();
            /* (ToT)
                        using (var stream = typeof(ThemeManager).Assembly.GetManifestResourceStream("Fluent.Themes.Themes.GeneratorParameters.json"))
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                return reader.ReadToEnd();
                            }
                        }
            */
        }
    }
}
