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
    public class MyResultsViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private int tournamentGroupId;
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private bool isRefreshing;
        private string filter;
        private List<Match> results;
        #endregion

        #region Properties
        public ObservableCollection<MatchItemViewModel> Results { get; set; }

        public string Filter
        {
            set
            {
                if (filter != value)
                {
                    filter = value;
                    if (string.IsNullOrEmpty(filter))
                    {
                        ReloadResults(results);
                    }

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Filter"));
                }
            }
            get
            {
                return filter;
            }
        }

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
        public MyResultsViewModel(int tournamentGroupId)
        {
            this.tournamentGroupId = tournamentGroupId;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            Results = new ObservableCollection<MatchItemViewModel>();

            LoadResults();
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        private async void LoadResults()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            IsRefreshing = true;
            var parameter = "http://apiligaveteranosjrz.azurewebsites.net/";
            var user = 3;
            var controller = string.Format("/Tournaments/GetMatchesToPredict/{0}/{1}", tournamentGroupId, user);
            var response = await apiService.Get<Match>(parameter, "/api", controller);
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            results = (List<Match>)response.Result;
            ReloadResults((List<Match>)response.Result);
        }

        private void ReloadResults(List<Match> results)
        {
            Results.Clear();

            foreach (var result in results)
            {
                Results.Add(new MatchItemViewModel
                {
                    DateId = result.DateId,
                    DateTime = result.DateTime,
                    Local = result.Local,
                    LocalGoals = result.LocalGoals,
                    LocalId = result.LocalId,
                    MatchId = result.MatchId,
                    StatusId = result.StatusId,
                    TournamentGroupId = result.TournamentGroupId,
                    Visitor = result.Visitor,
                    VisitorGoals = result.VisitorGoals,
                    VisitorId = result.VisitorId,
                    WasPredicted = result.WasPredicted,
                });
            }
        }

        private void Refresh()
        {
            LoadResults();
        }

        private void SearchResult()
        {
            var list = results
                .Where(r => r.Local.Initials.ToUpper() == Filter.ToUpper() ||
                            r.Visitor.Initials.ToUpper() == Filter.ToUpper())
                .ToList();
            ReloadResults(list);
        }

        #endregion

        #region Commands

        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        public ICommand SearchResultCommand { get { return new RelayCommand(SearchResult); } }

        #endregion
    }
}
