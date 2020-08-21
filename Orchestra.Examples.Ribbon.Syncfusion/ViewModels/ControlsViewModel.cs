﻿namespace Orchestra.Examples.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;
    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using Catel.Reflection;
    using Orc.Theming;

    public class ControlsViewModel: ViewModelBase
    {
        private readonly IAccentColorService _accentColorService;
        private readonly IBaseColorSchemeService _baseColorSchemeService;

        public ControlsViewModel(IAccentColorService accentColorService, IBaseColorSchemeService baseColorSchemeService)
        {
            Argument.IsNotNull(() => accentColorService);
            Argument.IsNotNull(() => baseColorSchemeService);

            _accentColorService = accentColorService;
            _baseColorSchemeService = baseColorSchemeService;

            AccentColors = typeof(Colors).GetPropertiesEx(true, true).Where(x => x.PropertyType.IsAssignableFromEx(typeof(Color))).Select(x => (Color)x.GetValue(null)).ToList();
            SelectedAccentColor = _accentColorService.GetAccentColor();

            BaseColorSchemes = _baseColorSchemeService.GetAvailableBaseColorSchemes();
            SelectedBaseColorScheme = BaseColorSchemes[0];
        }

        #region Properties
        public List<Color> AccentColors { get; private set; }
        public IReadOnlyList<string> BaseColorSchemes { get; private set; }

        private Color _selectedAccentColor;

        public Color SelectedAccentColor
        {
            get { return _selectedAccentColor; }
            set
            {
                if (_selectedAccentColor == value)
                    return;
                _selectedAccentColor = value;
                _accentColorService.SetAccentColor(value);
            }
        }
        public string SelectedBaseColorScheme { get; set; }

        public string Text { get; set; }
        #endregion

        #region Methods

        private void OnSelectedBaseColorSchemeChanged()
        {
            _baseColorSchemeService.SetBaseColorScheme(SelectedBaseColorScheme);
        }

        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            if (string.IsNullOrEmpty(Text))
            {
                validationResults.Add(new FieldValidationResult(nameof(Text), ValidationResultType.Error, "Text cannot be empty"));
            }

            base.ValidateFields(validationResults);
        }
        #endregion
    }
}
