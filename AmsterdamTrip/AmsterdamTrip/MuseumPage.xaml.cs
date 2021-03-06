using AmsterdamTrip.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AmsterdamTrip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MuseumPage : ContentPage
    {
        // =================================== VARIABLES ===================================

        private List<ImageButton> checkButtons = new List<ImageButton>();
        private List<Label> checkLabels = new List<Label>();
        private bool[] isChecked = new bool[99];
        private List<ImageButton> itemButtons = new List<ImageButton>();
        private List<StackLayout> itemLayouts = new List<StackLayout>();
        private MainPage mainPage { get; set; }

        private Museums selectedMuseum;
        private int currentIndex = 0;

        private StackLayout footer;
        private bool isPanelShowed = false;

        private StackLayout storageItems;

        // =================================================================================

        public MuseumPage(MainPage _mainPage)
        {
            InitializeComponent();

            mainPage = _mainPage;

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
                    Spacing = 8
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
                Text = "Museums",
                FontSize = 35,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            });

            header.Children.Add(new Image
            {
                Source = "Museum",
                Aspect = Aspect.AspectFit,
                HeightRequest = 50,
                WidthRequest = 50,
                VerticalOptions = LayoutOptions.Center
            });

            // Intégration du bouton ADD
            Button addButton;
            mainLayout.Children.Add(addButton = new Button
            {
                HeightRequest = 90,
                WidthRequest = 320,
                Margin = new Thickness(5),
                Padding = new Thickness(8),
                Text = "ADD",
                CornerRadius = 15,
                FontSize = 50,
                FontAttributes  = FontAttributes.Bold,
                BorderColor = Color.White,  
                BorderWidth = 5,
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            });
            addButton.Pressed += AddItem;

            Frame listItemsFrame;
            ScrollView listItems;
            StackLayout _storageItems;

            // Intégration du list items
            mainLayout.Children.Add(listItemsFrame = new Frame
            {
                Padding = new Thickness(5),
                BorderColor = Color.White,
                BackgroundColor = Color.Black,
                Content = listItems = new ScrollView
                {
                    Content = _storageItems = new StackLayout { }
                }
            });



            Debug.WriteLine(App.MuseumsRepository.StatusMessage);

            storageItems = _storageItems;
            GetMuseums();


            Frame footerFrame;

            // Intégration du frame footer
            mainLayout.Children.Add(footerFrame = new Frame
            {
                Padding = new Thickness(5),
                VerticalOptions = LayoutOptions.End,
                BorderColor = Color.White,
                BackgroundColor = Color.Black,
                Content = footer = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Padding = new Thickness(5),
                    Spacing = 8
                }
            });

            Content = mainLayout;
        }

        private async void GoToPreviousPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AddItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddItemPage(AddItemPage.Category.Museum, mainPage, this));
        }

        private void SelectItem(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;

            bool pass = true;
            int index = 0;

            itemLayouts[currentIndex].BackgroundColor = Color.Gray;

            while (pass)
            {
                if (itemButtons[index] == button)
                {
                    pass = false;
                }
                else
                {
                    index++;
                }
            }

            currentIndex = index;

            itemLayouts[currentIndex].BackgroundColor = Color.Purple;

            SelectItemAsync(index);

            ShowPanelButtons();
        }

        private async void SelectItemAsync(int _index)
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();

            selectedMuseum = museums[_index];
        }

        private void ShowPanelButtons()
        {
            if(isPanelShowed) { return; }

            isPanelShowed = true;

            // DELETE BUTTON
            Button deleteButton;
            footer.Children.Add(deleteButton = new Button
            {
                HeightRequest = 50,
                WidthRequest = 90,
                Margin = new Thickness(6),
                Padding = new Thickness(8),
                Text = "DELETE",
                CornerRadius = 10,
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                BorderColor = Color.White,
                BorderWidth = 2,
                VerticalOptions = LayoutOptions.Center
            });
            deleteButton.Pressed += DeleteItem;

            // MODIFY BUTTON
            Button modifyButton;
            footer.Children.Add(modifyButton = new Button
            {
                HeightRequest = 50,
                WidthRequest = 90,
                Margin = new Thickness(6),
                Padding = new Thickness(8),
                Text = "MODIFY",
                CornerRadius = 10,
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                BorderColor = Color.White,
                BorderWidth = 2,
                VerticalOptions = LayoutOptions.Center
            });
            modifyButton.Pressed += ModifyItem;

            // SHOW BUTTON
            Button showButton;
            footer.Children.Add(showButton = new Button
            {
                HeightRequest = 50,
                WidthRequest = 90,
                Margin = new Thickness(6),
                Padding = new Thickness(8),
                Text = "SHOW",
                CornerRadius = 10,
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                BorderColor = Color.White,
                BorderWidth = 2,
                VerticalOptions = LayoutOptions.Center
            });
            showButton.Pressed += ShowItem;
        }

        private async void DeleteItem(object sender, EventArgs e)
        {
            await App.MuseumsRepository.DeleteMuseumAsync(selectedMuseum);
            mainPage.SetMuseumsCount();
            await Navigation.PopAsync();
        }

        private async void ModifyItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddItemPage(AddItemPage.Category.Museum, mainPage, this, currentIndex));
        }

        private async void ShowItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PhotoPage(selectedMuseum.Name, AddItemPage.Category.Museum, currentIndex, selectedMuseum.Photo_01, selectedMuseum.Photo_02, selectedMuseum.Photo_03));
        }

        private void CheckItem(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;

            bool pass = true;
            int index = 0;

            while(pass)
            {
                if(checkButtons[index] == button)
                {
                    pass = false;
                }
                else
                {
                    index++;
                }
            }

            if(isChecked[index])
            {
                button.Source = "CheckButtonOff";
                isChecked[index] = false;

                checkLabels[index].Text = "";
            }
            else
            {
                button.Source = "CheckButtonOn";
                isChecked[index] = true;

                checkLabels[index].Text = DateTime.Today.ToString("d");
            }

            int intCheck = 0;
            if (isChecked[index]) { intCheck = 1; }

            SaveCheckItem(index, intCheck, checkLabels[index].Text);
        }

        private async void SaveCheckItem(int _index, int _isChecked, string _date)
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();

            await App.MuseumsRepository.CheckMuseumAsync(museums[_index], _isChecked, _date);

            mainPage.SetMuseumsCount();
        }

        public async void GetMuseums()
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();

            foreach (Museums museum in museums)
            {           
                StackLayout informationsLayout;
                StackLayout checkLayout;
                StackLayout hourlyAndAddressLayout;
                StackLayout itemLayout;

                storageItems.Children.Add(itemLayout = new StackLayout
                {
                    BackgroundColor = Color.Gray,
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                    Spacing = 2,
                    Orientation = StackOrientation.Horizontal
                });
                itemLayouts.Add(itemLayout);

                byte[] byteArray = museum.Image;
                MemoryStream stream = new MemoryStream(byteArray);

                ImageButton itemButton;
                itemLayout.Children.Add(itemButton = new ImageButton
                {
                    Source = ImageSource.FromStream(() => stream),
                    Aspect = Aspect.AspectFill,
                    HeightRequest = 90,
                    WidthRequest = 90,
                    VerticalOptions = LayoutOptions.Center
                });
                itemButtons.Add(itemButton);
                itemButton.Pressed += SelectItem;

                itemLayout.Children.Add(informationsLayout = new StackLayout
                {
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                    Spacing = 2,
                    Orientation = StackOrientation.Vertical
                });

                informationsLayout.Children.Add(new Label
                {
                    Text = museum.Name,
                    FontSize = 25,
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.End
                });

                Image imageExpectation;

                informationsLayout.Children.Add(imageExpectation = new Image
                {
                    Source = "Expectation0",
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 30,
                    WidthRequest = 90,
                    VerticalOptions = LayoutOptions.Center
                });

                if(museum.Expectation == 1) { imageExpectation.Source = "Expectation1"; }
                else if (museum.Expectation == 2) { imageExpectation.Source = "Expectation2"; }
                else if (museum.Expectation == 3) { imageExpectation.Source = "Expectation3"; }

                informationsLayout.Children.Add(hourlyAndAddressLayout = new StackLayout
                {
                    Spacing = 2,
                    Orientation = StackOrientation.Horizontal
                });

                hourlyAndAddressLayout.Children.Add(new Label{
                    Text = museum.Address,
                    FontSize = 10,
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center
                });

                hourlyAndAddressLayout.Children.Add(new Label
                {
                    Text = museum.Hourly,
                    FontSize = 10,
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center
                });

                itemLayout.Children.Add(checkLayout = new StackLayout
                {
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                    Spacing = 2,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    Orientation = StackOrientation.Vertical
                });

                ImageButton checkButton;
                Label checkDate;
                checkLayout.Children.Add(checkButton = new ImageButton
                {
                    Source = "CheckButtonOff",
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 60,
                    WidthRequest = 60
                });
                checkButtons.Add(checkButton);

                checkLayout.Children.Add(checkDate = new Label
                {
                    Text = "",
                    FontSize = 10,
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center
                });
                checkLabels.Add(checkDate);
                checkButton.Pressed += CheckItem;

                if (museum.IsChecked == 1)
                {
                    checkButton.Source = "CheckButtonOn";
                    checkDate.Text = museum.Date.ToString();
                }
            }
        }
    }
}