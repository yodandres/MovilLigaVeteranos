using FlagMovilPortable.Models;
using FlagMovilPortable.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace FlagMovilPortable.ViewModels
{
    public class GroupItemViewModel : Group
    {
        private NavigationService navigationService;

        public GroupItemViewModel()
        {
            navigationService = new NavigationService();
        }

        public ICommand SelectGroupCommand { get { return new RelayCommand(SelectGroup); } }

        private async void SelectGroup()
        {
            var mainViewModel = MainViewModel.GetInstance();
            var parameters = new Parameter();

            if (Application.Current.Properties.ContainsKey("Title"))
            {
                parameters.Option = Application.Current.Properties["Title"].ToString();
            }

            if (parameters.Option == "Tournaments Positions")
            {
                mainViewModel.Positions = new PositionsViewModel(TournamentGroupId);
                await navigationService.Navigate("PositionsPage");
            }
            else
            {
                mainViewModel.MyResults = new MyResultsViewModel(TournamentGroupId);
                await navigationService.Navigate("MyResultsPage");
            }
        }
    }
}
