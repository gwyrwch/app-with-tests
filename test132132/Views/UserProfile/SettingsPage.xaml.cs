using System;
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

            MessagingCenter.Subscribe<ContentPage, Models.User>(
                this, "EmailChanged", (obj, userWithNewEmail) =>
                {
                    viewModel.CurrentUser.Email = userWithNewEmail.Email;
                    App.UserChanged(viewModel.CurrentUser);
                    EmailLabel.BindingContext = App.GetCurrentUser();   
                });

            MessagingCenter.Subscribe<ContentPage, Models.User>(
                this, "UserNameChanged", (obj, newUser) =>
                {
                    viewModel.CurrentUser.UserName = newUser.UserName;
                    App.UserChanged(viewModel.CurrentUser);
                    UserNameLabel.BindingContext = App.GetCurrentUser();
                });
           // EmailLabel.BindingContext = App.GetCurrentUser();
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
            Application.Current.Resources["Silver"] = Color.Silver;
            Application.Current.Resources["MainColor"] = Color.FromHex("#007aff");
            Application.Current.Resources["SomeLightBackgroundColor"] = Color.FromHex("#f9f9f9");
            Application.Current.Resources["LightBackgroundColor"] = Color.FromHex("ffff");
            Application.Current.Resources["MainGreyColor"] = Color.FromHex("#8e8e93");
            App.MainImagePath = "GreyBlue.png";

            MessagingCenter.Send(this, "ChangeImage");
            MessagingCenter.Send(this, "RenderingAsked");
        }

        void DarkMode_Clicked(object sender, EventArgs e)
        {
            Application.Current.Resources["Silver"] = Color.FromHex("#ffcc00");
            Application.Current.Resources["MainColor"] = Color.FromHex("#5ac8fa");
            Application.Current.Resources["SomeLightBackgroundColor"] = Color.FromHex("#7e7e81");
            Application.Current.Resources["LightBackgroundColor"] = Color.FromHex("#8e8e93");
            Application.Current.Resources["MainGreyColor"] = Color.FromHex("#5ac8fa");
            App.MainImagePath = "lightblue.png";

            MessagingCenter.Send(this, "ChangeImage");
            MessagingCenter.Send(this, "RenderingAsked");
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
            return App.GetCurrentUser().Email;
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
