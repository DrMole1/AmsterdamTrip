using AmsterdamTrip.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AmsterdamTrip
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            SetMuseumsCount();
        }

        public async void SetMuseumsCount()
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();

            int checkedMuseum = 0;
            foreach(Museums museum in museums)
            {
                if(museum.IsChecked == 1) { checkedMuseum++; }
            }

            TotalMuseums.Text = checkedMuseum.ToString() + " / " + museums.Count.ToString();
        }

        private async void GoToMuseumPage(object sender, EventArgs e)
        {
            SetMuseumsCount();
            await Navigation.PushAsync(new MuseumPage(this));
        }

        private void GoToPlacePage(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new PlacePage());
        }

        private void GoToFoodPage(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new FoodPage());
        }

        private void GoToDrinkPage(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new DrinkPage());
        }
    }
}
