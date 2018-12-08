using System;
using System.Collections.Generic;

using Xamarin.Forms;

using ImageCircle.Forms.Plugin.iOS;


namespace test132132.Views.UserProfile
{
    public partial class UserInformationPage : ContentPage
    {
        ViewModels.User.UserProfileViewModel viewModel;

        public UserInformationPage()
        {
            InitializeComponent();
        
            viewModel = new ViewModels.User.UserProfileViewModel();
            Information.BindingContext = viewModel.CurrentUser;
        }

        async void Statistics_Tapped(object s, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticsPage(viewModel.CurrentUser.Stats));
        }


        async void Rating_Tapped(object s, EventArgs e)
        {
            await Navigation.PushAsync(new RatingPage());
        }
    }
}
