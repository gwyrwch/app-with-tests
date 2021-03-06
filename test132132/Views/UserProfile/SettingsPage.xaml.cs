﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace test132132.Views.UserProfile
{
    public partial class SettingsPage : ContentPage
    {
        ViewModels.User.UserProfileViewModel viewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.User.UserProfileViewModel();

            SettingsLayout.BindingContext = viewModel.CurrentUser;

            MessagingCenter.Subscribe<ContentPage, string>(
                this, "EmailChanged", (obj, email) =>
                {
                    App.CurrentUser.Email = email;
                    Common.UserBase.UpdateUser(App.CurrentUser);
                    viewModel.CurrentUser = App.CurrentUser;
                    EmailLabel.BindingContext = viewModel.CurrentUser;
                });

            MessagingCenter.Subscribe<ContentPage, string>(
                this, "UserNameChanged", (obj, username) =>
                {
                    // UserBase.NewUserName(App.CurrentUser.UserName, username)
                    App.CurrentUser.UserName = username;
                    Common.UserBase.UpdateUser(App.CurrentUser);
                    viewModel.CurrentUser = App.CurrentUser;
                    UserNameLabel.BindingContext = viewModel.CurrentUser;
                });
        }

        async void Email_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings.ChangeEmailPage());
        }

        async void UserName_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings.ChangeUserNamePage());
        }

        async void Support_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings.SupportPage());
        }

        async void Language_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings.ChangeLanguagePage());
        }

        void LightMode_Clicked(object sender, EventArgs e)
        {
            App.SetTheme("LightTheme");
            MessagingCenter.Send(this, "ChangesAsked");
            MessagingCenter.Send(this, "RenderingAsked");
            MessagingCenter.Send(this, "ChangeColorBottomBar");
        }

        void DarkMode_Clicked(object sender, EventArgs e)
        {
            App.SetTheme("DarkTheme");
            MessagingCenter.Send(this, "ChangesAsked");
            MessagingCenter.Send(this, "RenderingAsked");
            MessagingCenter.Send(this, "ChangeColorBottomBar");
        }
    }

    public class EmailConverter : IValueConverter 
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        { 
            var s = (string)value;
            if (string.IsNullOrEmpty(s))
                return "";
            //int lengthOfStars = s.Substring(0, s.IndexOf('@')).Length; //todo emails should have lenght > 3 without @...
            return string.Join("",              //todo this method with needed number of stars :)
                               s.Substring(0, 2),
                               new string('*', 2),
                               s.Substring(s.IndexOf('@') )
                   ); 
        } 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            //return App.GetCurrentUser().Email;
            return App.CurrentUser.Email;
       } 
    }


    public class UserNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            return "@" + s;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            if (string.IsNullOrEmpty(s))
                throw new Exception("username is null");
            return s.Substring(1);
        }
    }
}
