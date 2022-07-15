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
        public MuseumPage()
        {
            InitializeComponent();

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
            StackLayout storageItems;

            // Intégration du list items
            mainLayout.Children.Add(listItemsFrame = new Frame
            {
                Padding = new Thickness(5),
                BorderColor = Color.White,
                BackgroundColor = Color.Black,
                Content = listItems = new ScrollView
                {
                    Content = storageItems = new StackLayout { }
                }
            });



            Debug.WriteLine(App.MuseumsRepository.StatusMessage);

            GetMuseums(storageItems);


            Frame footerFrame;
            StackLayout footer;

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

            Content = mainLayout;
        }

        private async void GoToPreviousPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AddItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddItemPage(AddItemPage.Category.Museum));
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            Debug.WriteLine("Delete item");
        }

        private void ModifyItem(object sender, EventArgs e)
        {
            Debug.WriteLine("Modify item");
        }

        private void ShowItem(object sender, EventArgs e)
        {
            Debug.WriteLine("Show item");
        }

        private async void GetMuseums(StackLayout _storageItems)
        {
            List<Museums> museums = await App.MuseumsRepository.GetMuseumsAsync();

            foreach (Museums museum in museums)
            {
                StackLayout itemLayout;
                StackLayout informationsLayout;
                StackLayout checkLayout;

                _storageItems.Children.Add(itemLayout = new StackLayout
                {
                    BackgroundColor = Color.Gray,
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                    Spacing = 2,
                    Orientation = StackOrientation.Horizontal
                });

                byte[] byteArray = museum.Image;
                MemoryStream stream = new MemoryStream(byteArray);

                itemLayout.Children.Add(new Image
                {
                    Source = ImageSource.FromStream(() => stream),
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 50,
                    WidthRequest = 50,
                    VerticalOptions = LayoutOptions.Center
                });

                itemLayout.Children.Add(informationsLayout = new StackLayout
                {
                    BackgroundColor = Color.Gray,
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                    Spacing = 2,
                    Orientation = StackOrientation.Vertical
                });

                informationsLayout.Children.Add(new Label
                {
                    Text = museum.Name,
                    FontSize = 35,
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.End
                });
            }
        }
    }
}