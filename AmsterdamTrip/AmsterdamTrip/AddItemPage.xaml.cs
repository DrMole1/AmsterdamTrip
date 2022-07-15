
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;
using System.IO;
using Java.IO;

namespace AmsterdamTrip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public enum Category { Museum, Place, Food, Drink}

        // =============================== VARIABLES ===============================

        private int expectationLevel = 0;
        private bool isImageSet = false;
        private Category category = Category.Museum;
        private Stream stream;
        byte[] dataImage = null;

        // =========================================================================

        public AddItemPage(Category _cat)
        {
            InitializeComponent();

            category = _cat;

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
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo !"
            });

            stream = await result.OpenReadAsync();
            Stream streamSelection = await result.OpenReadAsync();

            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                dataImage = memory.ToArray();
            }

            EditImage.Source = ImageSource.FromStream(() => streamSelection);

            isImageSet = true;
        }


        private void CheckIfEmptySlot(object sender, EventArgs e)
        {
            bool canAddItem = true;

            if (string.IsNullOrEmpty(EditName.Text))
            {
                canAddItem = false;
                NameFrame.BackgroundColor = Color.Red;
            }
            else
            {
                NameFrame.BackgroundColor = Color.White;
            }


            if (string.IsNullOrEmpty(EditHourly.Text))
            {
                canAddItem = false;
                HourlyFrame.BackgroundColor = Color.Red;
            }
            else
            {
                HourlyFrame.BackgroundColor = Color.White;
            }


            if (string.IsNullOrEmpty(EditAddress.Text))
            {
                canAddItem = false;
                AddressFrame.BackgroundColor = Color.Red;
            }
            else
            {
                AddressFrame.BackgroundColor = Color.White;
            }


            if (isImageSet == false)
            {
                canAddItem = false;
                ImageFrame.BackgroundColor = Color.Red;
            }
            else
            {
                ImageFrame.BackgroundColor = Color.White;
            }

            if (canAddItem) { AddItem(); }
        }



        private async void AddItem()
        {
            await App.MuseumsRepository.AddNewMuseumAsync(EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel);

            Debug.WriteLine(App.MuseumsRepository.StatusMessage);

            await Navigation.PopAsync();
        }
    }
}