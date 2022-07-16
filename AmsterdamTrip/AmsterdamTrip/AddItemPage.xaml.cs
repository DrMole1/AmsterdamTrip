
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
using AmsterdamTrip.Models;

namespace AmsterdamTrip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public enum Category { Museum, Place, Food, Drink}

        // =============================== VARIABLES ===============================

        private int expectationLevel = 0;

        private bool isImageSet = false;
        private Stream stream;
        private byte[] dataImage = null;

        private MainPage mainPage;

        private bool usedToUpdate = false;

        private Category category = Category.Museum;

        private Museums currentMuseum;
        private Museums currentPlace;
        private Museums currentFood;
        private Museums currentDrink;

        private MuseumPage museumPage;
        private MuseumPage placePage;
        private MuseumPage foodPage;
        private MuseumPage drinkPage;


        // =========================================================================

        public AddItemPage(Category _cat, MainPage _mainPage, Page _previousPage)
        {
            InitializeComponent();

            mainPage = _mainPage;
            category = _cat;
            museumPage = (MuseumPage)_previousPage;

            Title.Text = "New " + _cat.ToString();
            ImageCat.Source = _cat.ToString();
            Debug.WriteLine("ADD");
        }

        public AddItemPage(Category _cat, MainPage _mainPage, Page _previousPage, int _index)
        {
            InitializeComponent();

            mainPage = _mainPage;
            category = _cat;
            usedToUpdate = true;
            museumPage = (MuseumPage)_previousPage;

            Title.Text = "Update " + _cat.ToString();
            ImageCat.Source = _cat.ToString();

            if(category == Category.Museum) { SetMuseumPage(_index); }
            else if (category == Category.Place) { SetPlacePage(_index); }
            else if (category == Category.Food) { SetFoodPage(_index); }
            else if (category == Category.Drink) { SetDrinkPage(_index); }
            Debug.WriteLine("index " + _index);
            Debug.WriteLine("index " + _index);
            Debug.WriteLine("index " + _index);
            Debug.WriteLine("index " + _index);
        }

        private async void SetMuseumPage(int _index)
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
            currentMuseum = museums[_index];

            EditName.Text = museums[_index].Name;
            EditAddress.Text = museums[_index].Address;
            EditHourly.Text = museums[_index].Hourly;

            if(museums[_index].Expectation == 1)
            {
                SetExpectation_01();
            }
            else if (museums[_index].Expectation == 2)
            {
                SetExpectation_02();
            }
            else if (museums[_index].Expectation == 3)
            {
                SetExpectation_03();
            }

            byte[] byteArray = museums[_index].Image;
            SetPhoto(byteArray);
        }

        private async void SetPlacePage(int _index)
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
            currentMuseum = museums[_index];

            EditName.Text = museums[_index].Name;
            EditAddress.Text = museums[_index].Address;
            EditHourly.Text = museums[_index].Hourly;

            if (museums[_index].Expectation == 1)
            {
                SetExpectation_01();
            }
            else if (museums[_index].Expectation == 2)
            {
                SetExpectation_02();
            }
            else if (museums[_index].Expectation == 3)
            {
                SetExpectation_03();
            }

            byte[] byteArray = museums[_index].Image;
            SetPhoto(byteArray);
        }

        private async void SetFoodPage(int _index)
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
            currentMuseum = museums[_index];

            EditName.Text = museums[_index].Name;
            EditAddress.Text = museums[_index].Address;
            EditHourly.Text = museums[_index].Hourly;

            if (museums[_index].Expectation == 1)
            {
                SetExpectation_01();
            }
            else if (museums[_index].Expectation == 2)
            {
                SetExpectation_02();
            }
            else if (museums[_index].Expectation == 3)
            {
                SetExpectation_03();
            }

            byte[] byteArray = museums[_index].Image;
            SetPhoto(byteArray);
        }

        private async void SetDrinkPage(int _index)
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
            currentMuseum = museums[_index];

            EditName.Text = museums[_index].Name;
            EditAddress.Text = museums[_index].Address;
            EditHourly.Text = museums[_index].Hourly;

            if (museums[_index].Expectation == 1)
            {
                SetExpectation_01();
            }
            else if (museums[_index].Expectation == 2)
            {
                SetExpectation_02();
            }
            else if (museums[_index].Expectation == 3)
            {
                SetExpectation_03();
            }

            byte[] byteArray = museums[_index].Image;
            SetPhoto(byteArray);
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

        private void SetExpectation_01()
        {
            expectationLevel = 1;

            Expectation_01.BackgroundColor = Color.Blue;
            Expectation_02.BackgroundColor = Color.White;
            Expectation_03.BackgroundColor = Color.White;
        }

        private void SetExpectation_02()
        {
            expectationLevel = 2;

            Expectation_01.BackgroundColor = Color.Purple;
            Expectation_02.BackgroundColor = Color.Purple;
            Expectation_03.BackgroundColor = Color.White;
        }

        private void SetExpectation_03()
        {
            expectationLevel = 3;

            Expectation_01.BackgroundColor = Color.Gold;
            Expectation_02.BackgroundColor = Color.Gold;
            Expectation_03.BackgroundColor = Color.Gold;
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

        private void SetPhoto(byte[] _dataByte)
        {
            MemoryStream stream = new MemoryStream(_dataByte);
            MemoryStream streamTemp = new MemoryStream(_dataByte);

            EditImage.Source = ImageSource.FromStream(() => streamTemp);

            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                dataImage = memory.ToArray();
            }

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
            if(!usedToUpdate)  // For CREATE
            {
                if (category == Category.Museum) { await App.MuseumsRepository.AddNewMuseumAsync(EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                if (category == Category.Place) { await App.MuseumsRepository.AddNewMuseumAsync(EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                if (category == Category.Food) { await App.MuseumsRepository.AddNewMuseumAsync(EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                if (category == Category.Drink) { await App.MuseumsRepository.AddNewMuseumAsync(EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                Debug.WriteLine("ADD");
            }
            else               // For UPDATE
            {
                if (category == Category.Museum) { await App.MuseumsRepository.UpdateMuseumAsync(currentMuseum, EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                if (category == Category.Place) { await App.MuseumsRepository.UpdateMuseumAsync(currentMuseum, EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                if (category == Category.Food) { await App.MuseumsRepository.UpdateMuseumAsync(currentMuseum, EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                if (category == Category.Drink) { await App.MuseumsRepository.UpdateMuseumAsync(currentMuseum, EditName.Text, dataImage, EditAddress.Text, EditHourly.Text, expectationLevel); }
                Debug.WriteLine("UPDATE");
            }

            if (category == Category.Museum)
            {
                mainPage.SetMuseumsCount();
                museumPage.GetMuseums();
            }
            if (category == Category.Place)
            {
                mainPage.SetMuseumsCount();
                museumPage.GetMuseums();
            }
            if (category == Category.Food)
            {
                mainPage.SetMuseumsCount();
                museumPage.GetMuseums();
            }
            if (category == Category.Drink)
            {
                mainPage.SetMuseumsCount();
                museumPage.GetMuseums();
            }

            //await Navigation.PushAsync(new MuseumPage(mainPage));
            await Navigation.PopToRootAsync();
        }
    }
}