namespace Orchestra.Examples.Ribbon.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        #region Constructors
        public MainView()
        {
// To prevent delay -- doesn't work after theme change (dark -> light)
//          SfSkinManager.SetTheme(this, new Theme(SyncfusionThemeConfig.ApplicationStartupTheme));
            InitializeComponent();
        }
        #endregion
    }
}