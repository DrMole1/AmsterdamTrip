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
        }

        private async void GoToMuseumPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MuseumPage());
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
