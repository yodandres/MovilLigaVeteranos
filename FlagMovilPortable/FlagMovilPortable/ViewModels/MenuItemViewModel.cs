namespace FlagMovilPortable.ViewModels
{
    using FlagMovilPortable.Models;
    using FlagMovilPortable.Services;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MenuItemViewModel
    {
        #region Attributes

        private NavigationService navigationService;

        #endregion

        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Constructors

        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }

        #endregion

        #region Commands

        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        private async void Navigate()
        {
            var mainViewModel = MainViewModel.GetInstance();

            var parameter = new Parameter()
            {
                PageName = PageName,
                Option = Title,
            };

            if (PageName == "LoginPage")
            {                
                mainViewModel.CurrentUser.IsRemembered = false;
                navigationService.SetMainPage(PageName);
            }
            else
            {
                switch (PageName)
                {
                    case "SelectTournamentPage":
                        parameter.Option = Title;
                        Application.Current.Properties["PageName"] = PageName;
                        Application.Current.Properties["Title"] = Title;
                        await Application.Current.SavePropertiesAsync();
                        mainViewModel.SelectTournament = new SelectTournamentViewModel();
                        await navigationService.Navigate(PageName);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
