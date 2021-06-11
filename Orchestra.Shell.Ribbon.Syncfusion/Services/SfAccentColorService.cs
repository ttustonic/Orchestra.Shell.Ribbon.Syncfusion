namespace Orchestra.Services
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    using Orc.Theming;
    using Syncfusion.SfSkinManager;

    public class SfBaseColorService: IBaseColorSchemeService
    {
        public event EventHandler<EventArgs> BaseColorSchemeChanged;
        string _currentScheme;
        static List<string> _schemes = new List<string>() { "Light", "Dark" };

        public SfBaseColorService()
        {
            var sfThemeName = SyncfusionThemeConfig
                .ApplicationTheme.ToString();

            if (sfThemeName.Contains("Light"))
                _currentScheme = "Light";
            else
                _currentScheme = "Dark";
        }

        public IReadOnlyList<string> GetAvailableBaseColorSchemes()
        {
            return _schemes;
        }

        public string GetBaseColorScheme()
        {
            return _currentScheme;
        }

        public bool SetBaseColorScheme(string scheme)
        {
            if (_currentScheme == scheme)
                return false;
            _currentScheme = scheme;
            var mainWin = Application.Current.MainWindow;
            Theme currentTheme = SfSkinManager.GetTheme(mainWin);
            string themeName = currentTheme.ThemeName;

            switch (scheme)
            {
                case "Dark":
                    themeName = themeName.Replace("Light", "Dark");
                    break;
                case "Light":
                    themeName = themeName.Replace("Dark", "Light");
                    break;
            }
            SyncfusionThemeConfig.ApplicationTheme = themeName;
            SfSkinManager.SetTheme(Application.Current.MainWindow, new Theme(themeName));
            _currentScheme = scheme;
            BaseColorSchemeChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }

    public class SfAccentColorService: AccentColorService
    {
        Color _currentAccentColor = default;

        public SfAccentColorService()
        { }

        public override Color GetAccentColor()
        {
            if (_currentAccentColor == default)
            {
                var primaryBrush =
                    Application.Current.TryFindResource("SystemAccentColor") as SolidColorBrush
                    ??
                    Application.Current.TryFindResource("PrimaryBackground") as SolidColorBrush;
                _currentAccentColor = primaryBrush?.Color ?? base.GetAccentColor();
            }
            return _currentAccentColor;
        }

        public override bool SetAccentColor(Color color)
        {
            if (color == _currentAccentColor)
                return false;
            var mainWin = Application.Current.MainWindow;
            Theme currentTheme = SfSkinManager.GetTheme(mainWin);

            var textColor = ControlzEx.Theming.RuntimeThemeGenerator.GetIdealTextColor(color);

            _currentAccentColor = color;
            var brush = new SolidColorBrush(color);
            var primaryForeground = new SolidColorBrush(textColor);

            IThemeSetting themeSetting = CreateThemeSetting(currentTheme, primaryForeground, brush);
            SfSkinManager.RegisterThemeSettings(currentTheme.ThemeName, themeSetting);
            SfSkinManager.SetTheme(mainWin, new Theme(currentTheme.ThemeName));
            return base.SetAccentColor(color);
        }

        IThemeSetting CreateThemeSetting(Theme theme, Brush foreground, Brush background)
        {
            var themeName = theme.ThemeName;
            var settingName = $"Syncfusion.Themes.{themeName}.WPF.{themeName}ThemeSettings, Syncfusion.Themes.{themeName}.WPF";
            var type = Type.GetType(settingName);
            var setting = Activator.CreateInstance(type) as IThemeSetting;

            var fgProp = type.GetProperty("PrimaryForeground");
            var bgProp = type.GetProperty("PrimaryBackground") ?? type.GetProperty("SystemAccentColor");

            fgProp?.SetValue(setting, foreground);
            bgProp?.SetValue(setting, background);

            return setting;
        }
    }
}
