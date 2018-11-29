using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile.Settings
{
    public partial class ChangeEmailPage : ContentPage
    {
        ViewModels.User.EmailChangeViewModel viewModel;
        public ChangeEmailPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ViewModels.User.EmailChangeViewModel();
            ChangeEmailLayout.BindingContext = viewModel.CurrentUser;
        }

        async void Done_Clicked(object sender, EventArgs e)
        {
            try
            {
                viewModel.CurrentUser.Email = newEmailEntry.Text;
            }
            catch (Exception exc)
            {
                await DisplayAlert(iOS.AppResources.ChangeEmailPageInvalidEmail, exc.Message, iOS.AppResources.CommonOk); //todo set email in
                return;
            }

            MessagingCenter.Send(
                (ContentPage)this,
                "EmailChanged",
                viewModel.CurrentUser.Email
            );

            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            //Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync()); //todo why 2 times?
        }
    }
}
