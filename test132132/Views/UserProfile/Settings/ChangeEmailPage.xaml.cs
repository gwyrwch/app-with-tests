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
            //userNameViewCell.BindingContext = viewModel.currentUser;
            ChangeEmailLayout.BindingContext = viewModel.CurrentUser;
        }

        async void Done_Clicked(object sender, EventArgs e)
        {
           // Models.User newUser = new Models.User();
            try
            {
                viewModel.CurrentUser.Email = newEmailEntry.Text;
            }
            catch (Exception exc)
            {
                await DisplayAlert("Invalid email", exc.Message, "Ok"); //todo set email in
                return;
            }

            MessagingCenter.Send(
                (ContentPage)this,
                "EmailChanged",
                viewModel.CurrentUser
            );
            App.UserChanged(viewModel.CurrentUser);

            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            //Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync()); //todo why 2 times?
        }
    }
}
