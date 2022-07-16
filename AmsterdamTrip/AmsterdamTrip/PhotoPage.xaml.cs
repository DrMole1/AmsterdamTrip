using AmsterdamTrip.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static AmsterdamTrip.AddItemPage;

namespace AmsterdamTrip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoPage : ContentPage
    {
        // ================================ VARIABLES ================================

        private string title = "";
        private Category category = Category.Museum;
        private int index = 0;
        private byte[] photos01;
        private byte[] photos02;
        private byte[] photos03;

        private StackLayout storageItems;

        // ===========================================================================


        public PhotoPage(string _title, Category _cat, int _index, byte[] _photos01, byte[] _photos02, byte[] _photos03)
        {
            InitializeComponent();

            title = _title;
            category = _cat;
            index = _index;
            photos01 = _photos01; 
            photos02 = _photos02;
            photos03 = _photos03;

            StackLayout mainLayout = new StackLayout
            {
                BackgroundColor = Color.Gray,
                Margin = new Thickness(10),
                Padding = new Thickness(8),
                Spacing = 10,
                Orientation = StackOrientation.Vertical
            };

            Frame headerFrame;
            StackLayout header;

            // Intégration du frame header
            mainLayout.Children.Add(headerFrame = new Frame
            {
                Padding = new Thickness(5),
                VerticalOptions = LayoutOptions.Start,
                BorderColor = Color.White,
                BackgroundColor = Color.Black,
                Content = header = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Padding = new Thickness(5),
                    VerticalOptions = LayoutOptions.Start,
                    Spacing = 16
                }
            });

            Button previousButton;
            header.Children.Add(previousButton = new Button
            {
                HeightRequest = 50,
                WidthRequest = 90,
                Margin = new Thickness(6),
                Padding = new Thickness(8),
                Text = "Previous",
                CornerRadius = 10,
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                BorderColor = Color.White,
                BorderWidth = 2,
                VerticalOptions = LayoutOptions.Center
            });
            previousButton.Pressed += GoToPreviousPage;

            header.Children.Add(new Label
            {
                Text = title,
                FontSize = 35,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            });

            header.Children.Add(new Image
            {
                Source = category.ToString(),
                Aspect = Aspect.AspectFit,
                HeightRequest = 50,
                WidthRequest = 50,
                VerticalOptions = LayoutOptions.Center
            });

            Frame listItemsFrame;
            ScrollView listItems;

            // Intégration du list items
            mainLayout.Children.Add(listItemsFrame = new Frame
            {
                Padding = new Thickness(5),
                BorderColor = Color.White,
                BackgroundColor = Color.Black,
                Content = listItems = new ScrollView
                {
                    Content = storageItems = new StackLayout 
                    {
                    }
                }
            });

            SetPhotos();

            Content = mainLayout;
        }

        private async void GoToPreviousPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void SetPhotos()
        {
            ImageButton buttonPhoto_01;
            ImageButton buttonPhoto_02;
            ImageButton buttonPhoto_03;

            storageItems.Children.Add(buttonPhoto_01 = new ImageButton
            {
                Source = "AddImage",
                Aspect = Aspect.AspectFill,
                HeightRequest = 300,
                WidthRequest = 300,
                VerticalOptions = LayoutOptions.Center
            });

            storageItems.Children.Add(buttonPhoto_02 = new ImageButton
            {
                Source = "AddImage",
                Aspect = Aspect.AspectFill,
                HeightRequest = 300,
                WidthRequest = 300,
                VerticalOptions = LayoutOptions.Center
            });

            storageItems.Children.Add(buttonPhoto_03 = new ImageButton
            {
                Source = "AddImage",
                Aspect = Aspect.AspectFill,
                HeightRequest = 300,
                WidthRequest = 300,
                VerticalOptions = LayoutOptions.Center
            });

            buttonPhoto_01.Released += UploadPhoto_01;
            buttonPhoto_02.Released += UploadPhoto_02;
            buttonPhoto_03.Released += UploadPhoto_03;

            if(photos01 != null) 
            {
                MemoryStream stream1 = new MemoryStream(photos01);
                buttonPhoto_01.Source = ImageSource.FromStream(() => stream1);
            }

            if (photos02 != null)
            {
                MemoryStream stream2 = new MemoryStream(photos02);
                buttonPhoto_02.Source = ImageSource.FromStream(() => stream2);
            }

            if (photos03 != null)
            {
                MemoryStream stream3 = new MemoryStream(photos03);
                buttonPhoto_03.Source = ImageSource.FromStream(() => stream3);
            }
        }

        private async void UploadPhoto_01(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo !"
            });

            Stream stream = await result.OpenReadAsync();
            Stream streamSelection = await result.OpenReadAsync();
            byte[] dataImage;

            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                dataImage = memory.ToArray();
            }

            ImageButton button = (ImageButton)sender;
            button.Source = ImageSource.FromStream(() => streamSelection);

            if(category == Category.Museum) 
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 1, dataImage); 
            }
            if (category == Category.Place)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 1, dataImage);
            }
            if (category == Category.Food)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 1, dataImage);
            }
            if (category == Category.Drink)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 1, dataImage);
            }
        }



        private async void UploadPhoto_02(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo !"
            });

            Stream stream = await result.OpenReadAsync();
            Stream streamSelection = await result.OpenReadAsync();
            byte[] dataImage;

            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                dataImage = memory.ToArray();
            }

            ImageButton button = (ImageButton)sender;
            button.Source = ImageSource.FromStream(() => streamSelection);

            if (category == Category.Museum)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 2, dataImage);
            }
            if (category == Category.Place)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 2, dataImage);
            }
            if (category == Category.Food)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 2, dataImage);
            }
            if (category == Category.Drink)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 2, dataImage);
            }
        }



        private async void UploadPhoto_03(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo !"
            });

            Stream stream = await result.OpenReadAsync();
            Stream streamSelection = await result.OpenReadAsync();
            byte[] dataImage;

            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                dataImage = memory.ToArray();
            }

            ImageButton button = (ImageButton)sender;
            button.Source = ImageSource.FromStream(() => streamSelection);

            if (category == Category.Museum)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 3, dataImage);
            }
            if (category == Category.Place)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 3, dataImage);
            }
            if (category == Category.Food)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 3, dataImage);
            }
            if (category == Category.Drink)
            {
                List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();
                Museums selectedMuseum = museums[index];
                await App.MuseumsRepository.AddPhotoMuseumAsync(selectedMuseum, 3, dataImage);
            }
        }
    }
}