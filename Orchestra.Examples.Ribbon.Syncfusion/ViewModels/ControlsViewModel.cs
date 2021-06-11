namespace Orchestra.Examples.Ribbon.ViewModels
{
    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using Orc.Theming;
    using System.Collections.Generic;

    public class ControlsViewModel : ViewModelBase
    {
        public ControlsViewModel(IAccentColorService accentColorService, IBaseColorSchemeService baseColorSchemeService)
        {
            Argument.IsNotNull(() => accentColorService);
            Argument.IsNotNull(() => baseColorSchemeService);
        }

        #region Properties

        public string Text { get; set; }
        #endregion

        #region Methods

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
