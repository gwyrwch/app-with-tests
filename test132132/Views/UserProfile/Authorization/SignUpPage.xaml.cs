using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.UserProfile.Authorization
{
    public partial class SignUpPage : ContentPage
    {
        ViewModels.User.AuthrizationViewModel viewModel;
        public SignUpPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ViewModels.User.AuthrizationViewModel();
        }
    }
}
