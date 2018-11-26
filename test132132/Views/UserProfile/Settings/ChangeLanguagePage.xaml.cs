using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile.Settings
{
    public partial class ChangeLanguagePage : ContentPage
    {
        public ChangeLanguagePage()
        {
            InitializeComponent();
        }

        void English_Tapped(object sender, EventArgs e)
        {
            RussianPlus.IsVisible = false;
            EnglishPlus.IsVisible = true;
        }

        void Russian_Tapped(object sender, EventArgs e) 
        {
            // русский значит трезвый
            RussianPlus.IsVisible = true;
            EnglishPlus.IsVisible = false;
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            string newLanguage;
            if (RussianPlus.IsVisible)
            {
                newLanguage = "Russian";
            } else {
                newLanguage = "English";
            }
            App.settings.Language = newLanguage;
            App.SaveSettings();

            await DisplayAlert("Language was changed", "Reload the application to see the changes", "OK");
            await Navigation.PopAsync();
        }
    }
}
