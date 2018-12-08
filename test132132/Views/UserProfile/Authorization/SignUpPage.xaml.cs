using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace test132132.Views.UserProfile.Authorization
{
    public partial class SignUpPage : ContentPage
    {
        Models.User signUppingUser;
        public SignUpPage()
        {
            signUppingUser = new Models.User();
            InitializeComponent();
            BindingContext = signUppingUser;
        }

        async void Photo_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                signUppingUser.ProfileImagePath = photo.Path;
                ProfileImage.BindingContext = signUppingUser;
            }
        }

        async void SignUp_Clicked(object sender, EventArgs e)
        {
            string password = PasswordEntry.Text;
            if (!Common.UserBase.GoodPassword(password))
            {
                await DisplayAlert(
                    "Bad password", 
                    "Password should be at least 5 characters long", 
                    iOS.AppResources.CommonOk
                );
                return;
            }

            try {
                signUppingUser.Name = NameEntry.Text;
                signUppingUser.Surname = SurnameEntry.Text;
                signUppingUser.Email = EmailEntry.Text;
                signUppingUser.Birth = BirthdayPicker.Date;
                signUppingUser.Education = EducationEntry.Text;
                signUppingUser.UserName = UserNameEntry.Text;
                signUppingUser.Stats = new Models.UserStatistics();
            }
            catch(Exception exc)
            {
                await DisplayAlert(
                    iOS.AppResources.SignUpPageInvalidUsername, 
                    exc.Message, 
                    iOS.AppResources.CommonOk
                );  
                return;
            }

            Common.UserBase.NewUser(signUppingUser, password);
            MessagingCenter.Send(new SettingsPage(), "RenderingAsked");
        }
    }
}
