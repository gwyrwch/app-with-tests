using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile.Authorization
{
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            MainImage.Source = App.MainImagePath;

            MessagingCenter.Subscribe<Views.UserProfile.SettingsPage>(this, "ChangeImage",
                (obj) => {
                    ImagePathChanged();
                }
            );

        }

        public void ImagePathChanged()
        {
            MainImage.Source = App.MainImagePath;
        }

        public async void SignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        public async void LogIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
        }
    }
}
