using FlagMovilPortable.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlagMovilPortable.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectMatchPage : ContentPage
    {
        public SelectMatchPage()
        {
            InitializeComponent();

            var selectMatchViewModel = SelectMatchViewModel.GetInstance();
            base.Appearing += (object sender, EventArgs e) =>
            {
                selectMatchViewModel.RefreshCommand.Execute(this);
            };
        }
    }
}