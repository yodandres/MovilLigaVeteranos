namespace FlagMovilPortable.ViewModels
{
    using FlagMovilPortable.Models;
    using System.Collections.ObjectModel;

    public class MainViewModel
    {
        #region Properties

        public SelectTournamentViewModel SelectTournament { get; set; }

        public SelectMatchViewModel SelectMatch { get; set; }

        public SelectGroupViewModel SelectGroup { get; set; }

        public PositionsViewModel Positions { get; set; }

        public MyResultsViewModel MyResults { get; set; }

        public User CurrentUser { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region Constructors

        public MainViewModel()
        {
            instance = this;           
            LoadMenu();
        }
        #endregion

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods

        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

            Menu.Add(new MenuItemViewModel
            {
                Icon = "predictions.png",
                PageName = "SelectTournamentPage",
                Title = "Games",
            });

            ////Menu.Add(new MenuItemViewModel
            ////{
            ////    Icon = "groups.png",
            ////    PageName = "GroupsPage",
            ////    Title = "Groups",
            ////});

            Menu.Add(new MenuItemViewModel
            {
                Icon = "tournaments.png",
                PageName = "SelectTournamentPage",
                Title = "Tournaments Positions",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "myresults.png",
                PageName = "SelectTournamentPage",
                Title = "Games Results",
            });

            ////Menu.Add(new MenuItemViewModel
            ////{
            ////    Icon = "config.png",
            ////    PageName = "ConfigPage",
            ////    Title = "Config",
            ////});

            ////Menu.Add(new MenuItemViewModel
            ////{
            ////    Icon = "logut.png",
            ////    PageName = "LoginPage",
            ////    Title = "Logut",
            ////});

        }
        #endregion
    }
}
