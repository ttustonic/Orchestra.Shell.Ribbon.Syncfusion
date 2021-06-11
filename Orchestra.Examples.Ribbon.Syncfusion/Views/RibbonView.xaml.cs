namespace Orchestra.Examples.Ribbon.Views
{
    public partial class RibbonView
    {
        #region Constructors
        public RibbonView()
        {
            InitializeComponent();
            // (ToT)
            //            ribbon.AddAboutButton();
        }
        #endregion

        #region Methods
        protected override void OnViewModelChanged()
        {
            base.OnViewModelChanged();

#pragma warning disable WPF0041
/* (ToT)
            backstageTabControl.DataContext = ViewModel;
*/
#pragma warning restore WPF0041
        }
        #endregion
    }
}
