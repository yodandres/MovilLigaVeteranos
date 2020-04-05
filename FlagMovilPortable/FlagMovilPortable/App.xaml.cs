namespace FlagMovilPortable
{
    using FlagMovilPortable.Pages;
    using Xamarin.Forms;

    public partial class App : Application
    {
        #region Properties

        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        #endregion

        #region Contructors

        public App()
        {
            InitializeComponent();

            MainPage = new MasterPage();
        }

        #endregion    

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
