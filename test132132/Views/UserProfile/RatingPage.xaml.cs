using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile
{
    public partial class RatingPage : ContentPage
    {
        ViewModels.User.RatingPageViewModel vm;
        public RatingPage()
        {
            InitializeComponent();

            vm = new ViewModels.User.RatingPageViewModel();

            //top line in Grid
            layoutForGrid.Children.Add(CreateLineForGrid());

            layoutForGrid.Children.Add(CreateGrid());

            //bottom line in Grid
            layoutForGrid.Children.Add(CreateLineForGrid());
        }

        Label CreateLineForGrid()
        {
            return new Label
            {
                HeightRequest = 1,
                WidthRequest = Application.Current.MainPage.Width,
                BackgroundColor = (Color)Application.Current.Resources["TextColor"]
            };
        }

        public Grid CreateGrid()
        {
            var grid = new Grid()
            {
                BackgroundColor = (Color)Application.Current.Resources["TextColor"],
                RowSpacing = 1,
                ColumnSpacing = 1
            };
            List<Tuple<string, int>> usersList = vm.SortedUsers();

            grid.ColumnDefinitions.Add(
                        new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) }
                    );
            grid.ColumnDefinitions.Add(
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    );
            grid.ColumnDefinitions.Add(
                        new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) }
                    );
            //first row
            grid.RowDefinitions.Add(
                        new RowDefinition { Height = new GridLength(Application.Current.MainPage.Height / 30) }
                    );
            grid.Children.Add(
                CreateCell(" "), 0, 0
            );
            grid.Children.Add(
                CreateCell(iOS.AppResources.RatingPageUser), 1, 0
            );
            grid.Children.Add(
                CreateCell(iOS.AppResources.UserInformationPageRating), 2, 0
            );

            for (int j = 1; j <= usersList.Count; j++)
            {
                grid.RowDefinitions.Add(
                        new RowDefinition { Height = new GridLength(Application.Current.MainPage.Height / 30) }
                    );
                grid.Children.Add(
                    CreateCell((j).ToString()), 0, j
                );
                grid.Children.Add(
                    CreateCell(usersList[j - 1].Item1), 1, j
                );
                grid.Children.Add(
                    CreateCell(usersList[j - 1].Item2.ToString()), 2, j
                );
            }
                return grid;
        }

        Label CreateCell(string labelText)
        {
            return new Label()
            {
                Text = labelText,
                TextColor = (Color)Application.Current.Resources["TextColor"],
                BackgroundColor = (Color)Application.Current.Resources["SomeLightBackgroundColor"],
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
        }
    }
}
