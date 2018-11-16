using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile.Authorization
{
    public partial class SignUpPage : ContentPage
    {
        ViewModels.User.UserProfileViewModel viewModel;
        public SignUpPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ViewModels.User.UserProfileViewModel();
        }

        async void SignUp_Clicked(object sender, EventArgs e)
        {   
            try{
                viewModel.CurrentUser.Name = NameEntry.Text;
            }
            catch(Exception exc)
            {
                await DisplayAlert("Invalid username", exc.Message, "Ok");  
                return;
            }


            App.UserChanged(viewModel.CurrentUser);

            await Navigation.PushAsync(new ProfilePage());
        }


    }
}
