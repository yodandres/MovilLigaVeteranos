namespace FlagMovilPortable.ViewModels
{
    using FlagMovilPortable.Models;
    using FlagMovilPortable.Services;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TournamentItemViewModel : Tournament
    {
        private NavigationService navigationService;
        public Parameter parameter;
        public string Title { get; set; }
        public string PageName { get; set; }

        public TournamentItemViewModel()
        {
            navigationService = new NavigationService();
            parameter = new Parameter(); 
        }

        public ICommand SelectTournamentCommand { get { return new RelayCommand(SelectTournament); } }

        private async void SelectTournament()
        {
            var mainViewModel = MainViewModel.GetInstance();
            parameter = new Parameter();
            
            if (Application.Current.Properties.ContainsKey("Title"))
            {
                parameter.Option = Application.Current.Properties["Title"].ToString();
            }             

            if (parameter.Option == "Games")
            {
                mainViewModel.SelectMatch = new SelectMatchViewModel(TournamentId);
                await navigationService.Navigate("SelectMatchPage");
            }
            else
            {
                if (parameter.Option == "Tournaments Positions")
                {
                    Application.Current.Properties["Title"] = parameter.Option;
                    await Application.Current.SavePropertiesAsync();
                }

                mainViewModel.SelectGroup = new SelectGroupViewModel(Groups);
                await navigationService.Navigate("SelectGroupPage");
            }
        }
    }
}
