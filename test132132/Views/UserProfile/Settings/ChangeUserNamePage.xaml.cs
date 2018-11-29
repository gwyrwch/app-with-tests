using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile.Settings
{
    public partial class ChangeUserNamePage : ContentPage
    {
        ViewModels.User.UserProfileViewModel viewModel;
        public ChangeUserNamePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.User.UserProfileViewModel();

            ChangeUserNameLayout.BindingContext = viewModel.CurrentUser;
        }

        async void Done_Clicked(object sender, EventArgs e)
        {
            return;
            try
            {
                viewModel.CurrentUser.UserName = newUserNameEntry.Text;
            }
            catch (Exception exc)
            {
                await DisplayAlert(
                    iOS.AppResources.SignUpPageInvalidUsername, 
                    exc.Message, 
                    iOS.AppResources.CommonOk
                ); //todo set userName 
                return;
            }

            MessagingCenter.Send(
                (ContentPage)this,
                "UserNameChanged",
                viewModel.CurrentUser.UserName
            );
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            //Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
        }
    }
}
