using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile.Authorization
{
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        async void LogIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
    }
}
