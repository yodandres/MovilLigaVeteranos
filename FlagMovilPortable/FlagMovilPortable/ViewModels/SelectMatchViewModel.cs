using FlagMovilPortable.Models;
using FlagMovilPortable.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlagMovilPortable.ViewModels
{
    public class SelectMatchViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private int tournamentId;
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private bool isRefreshing;

        #endregion

        #region Properties

        public ObservableCollection<MatchItemViewModel> Matches { get; set; }

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

        #region Contructors

        public SelectMatchViewModel(int tournamentId)
        {
            instance = this;
            this.tournamentId = tournamentId;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            Matches = new ObservableCollection<MatchItemViewModel>();
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Singleton

        private static SelectMatchViewModel instance;

        public static SelectMatchViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Methods
        private async void LoadMatches()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            IsRefreshing = true;
            var parameter = "http://apiligaveteranosjrz.azurewebsites.net/";
            var user = 3;
            var controller = string.Format("/Tournaments/GetMatchesToPredict/{0}/{1}", tournamentId, user);
            var response = await apiService.Get<Match>(parameter, "/api", controller);
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ReloadMatches((List<Match>)response.Result);
        }

        private void ReloadMatches(List<Match> matches)
        {
            Matches.Clear();

            foreach (var match in matches)
            {
                Matches.Add(new MatchItemViewModel
                {
                    DateId = match.DateId,
                    DateTime = match.DateTime,
                    Local = match.Local,
                    LocalGoals = match.LocalGoals,
                    LocalId = match.LocalId,
                    MatchId = match.MatchId,
                    StatusId = match.StatusId,
                    TournamentGroupId = match.TournamentGroupId,
                    Visitor = match.Visitor,
                    VisitorGoals = match.VisitorGoals,
                    VisitorId = match.VisitorId,
                    WasPredicted = match.WasPredicted,
                });
            }
        }

        private void Refresh()
        {
            LoadMatches();
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }
        
        #endregion
    }
}
