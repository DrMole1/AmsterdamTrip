
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;

namespace AmsterdamTrip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public enum Category { Museum, Place, Food, Drink}

        // =============================== VARIABLES ===============================

        private int expectationLevel = 0;

        // =========================================================================

        public AddItemPage(Category _cat)
        {
            InitializeComponent();

            Title.Text = "New " + _cat.ToString();
            ImageCat.Source = _cat.ToString();
        }


        private async void GoToPreviousPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Expectation_01_Clicked(object sender, EventArgs e)
        {
            if(expectationLevel == 1)
            {
                expectationLevel = 0;

                Expectation_01.BackgroundColor = Color.White;
                Expectation_02.BackgroundColor = Color.White;
                Expectation_03.BackgroundColor = Color.White;
            }
            else
            {
                expectationLevel = 1;

                Expectation_01.BackgroundColor = Color.Blue;
                Expectation_02.BackgroundColor = Color.White;
                Expectation_03.BackgroundColor = Color.White;
            }
        }

        private void Expectation_02_Clicked(object sender, EventArgs e)
        {
            if (expectationLevel == 2)
            {
                expectationLevel = 1;

                Expectation_01.BackgroundColor = Color.Blue;
                Expectation_02.BackgroundColor = Color.White;
                Expectation_03.BackgroundColor = Color.White;
            }
            else
            {
                expectationLevel = 2;

                Expectation_01.BackgroundColor = Color.Purple;
                Expectation_02.BackgroundColor = Color.Purple;
                Expectation_03.BackgroundColor = Color.White;
            }
        }

        private void Expectation_03_Clicked(object sender, EventArgs e)
        {
            if (expectationLevel == 3)
            {
                expectationLevel = 2;

                Expectation_01.BackgroundColor = Color.Purple;
                Expectation_02.BackgroundColor = Color.Purple;
                Expectation_03.BackgroundColor = Color.White;
            }
            else
            {
                expectationLevel = 3;

                Expectation_01.BackgroundColor = Color.Gold;
                Expectation_02.BackgroundColor = Color.Gold;
                Expectation_03.BackgroundColor = Color.Gold;
            }
        }

        private async void UploadPhoto(object sender, EventArgs e)
        {
            
        }
    }
}