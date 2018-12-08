using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            string emailOrUsername = EmailOrUsername.Text;
            string password = Password.Text;

            if (Common.UserBase.TryToLogin(emailOrUsername, password))
            {
                MessagingCenter.Send(new SettingsPage(), "RenderingAsked");
                //await Navigation.PushAsync(new ProfilePage());
                //Page p = new ProfilePage();
                //NavigationPage.SetHasBackButton(p, false);

                //await Navigation.PushAsync(p);
            }
            else
            {
                await DisplayAlert("Try again", "Wrong username or password", iOS.AppResources.CommonOk); // fixme: Translate
            }
        }
    }
}
