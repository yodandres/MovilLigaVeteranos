namespace FlagMovilPortable.ViewModels
{
    using FlagMovilPortable.Models;
    using FlagMovilPortable.Services;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;

    public class MatchItemViewModel : Match
    {
        //// esta clase es para selecionar un juego para predecir
        ////#region Attributes
        ////private NavigationService navigationService;
        ////#endregion

        ////#region Constructor
        ////public MatchItemViewModel()
        ////{
        ////    navigationService = new NavigationService();
        ////}
        ////#endregion

        ////#region Events
        ////public ICommand SelectMatchCommand { get { return new RelayCommand(SelectMatch); } }
        ////#endregion

        ////#region Methods
        ////private async void SelectMatch()
        ////{
        ////    var mainViewModel = MainViewModel.GetInstance();
        ////    mainViewModel.EditPrediction = new EditPredictionViewModel(this);
        ////    await navigationService.Navigate("EditPredictionPage");
        ////}
        ////#endregion
    }
}
