using FlagMovilPortable.Models;
using FlagMovilPortable.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace FlagMovilPortable.ViewModels
{
    public class PositionsViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Attributes

        private ApiService apiService;
        private DialogService dialogService;
        private bool isRefreshing;
        private int tournamentGroupId;
        #endregion

        #region Properties

        public ObservableCollection<TournamentTeamItemViewModel> TournamentTeam { get; set; }

        public bool IsRefreshing
        {
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
            get
            {
                return isRefreshing;
            }
        }

        #endregion

        #region Constructor
        public PositionsViewModel(int tournamentGroupId)
        {
            this.tournamentGroupId = tournamentGroupId;
            apiService = new ApiService();
            dialogService = new DialogService();

            TournamentTeam = new ObservableCollection<TournamentTeamItemViewModel>();

            LoadTournamentsTeam();
        }
        #endregion

        #region Methods
        private async void LoadTournamentsTeam()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            IsRefreshing = true;
            var parameters = "http://apiligaveteranosjrz.azurewebsites.net/";

            var response = await apiService.Get<TournamentTeam>(parameters, "/api", "/TournamentTeams", tournamentGroupId);
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ReloadTournamentTeams((List<TournamentTeam>)response.Result);
        }

        private void ReloadTournamentTeams(List<TournamentTeam> tournamentTeams)
        {
            TournamentTeam.Clear();
            foreach (var tournamentTeam in tournamentTeams)
            {
                TournamentTeam.Add(new TournamentTeamItemViewModel()
                {
                    AgainstGoals = tournamentTeam.AgainstGoals,
                    FavorGoals = tournamentTeam.FavorGoals,
                    MatchesPlayed = tournamentTeam.MatchesPlayed,
                    MatchesLost = tournamentTeam.MatchesLost,
                    MatchesTied = tournamentTeam.MatchesTied,
                    MatchesWon = tournamentTeam.MatchesWon,
                    Points = tournamentTeam.Points,
                    Position = tournamentTeam.Position,
                    Team = tournamentTeam.Team,
                    TeamId = tournamentTeam.TeamId,
                    TournamentGroupId = tournamentTeam.TournamentGroupId,
                    TournamentTeamId = tournamentTeam.TournamentTeamId,
                });
            }
        }

        private void Refresh()
        {
            LoadTournamentsTeam();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        #endregion
    }
}
