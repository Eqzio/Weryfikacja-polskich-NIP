using PUM.Models;
using PUM.ViewModels;
using PUM.Views;
using System.ComponentModel;
using Xamarin.Forms;

namespace PUM
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(Navigation);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            VerificationHistoryListView.ItemsSource = await App.Database.GetVerifiedNIPAsync();
        }

        private async void VerificationHistoryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new DetailsPage
                {
                    BindingContext = e.SelectedItem as VerificationDetails.VerifiedNIP
                });
            }
        }
    }
}
