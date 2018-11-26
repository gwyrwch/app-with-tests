using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132
{
    public partial class ProfilePage : MasterDetailPage
    {
      // ViewModels.User.UserProfileViewModel viewModel; todo
        public ProfilePage()
        {
            //Detail.BindingContext = viewModel = new ViewModels.User.UserProfileViewModel();
            //Master.BindingContext = viewModel; //todo that's right but doesn't work
            //BindingContext = viewModel = new ViewModels.User.UserProfileViewModel(); todo
            InitializeComponent();


            //MessagingCenter.Subscribe<ContentPage, Models.User>(
            //this, "EmailChanged", (obj, userWithNewEmail) =>
            //{
            //    viewModel.currentUser.Email = userWithNewEmail.Email;
            //    // EmailLabel.SetBinding(lavel.TextProperty, new Binding("Email", BindingMode.TwoWay, new EmailConverter()));
            //    BindingContext = viewModel.currentUser;
            //});

        }

        void Profile_Tapped(object sender, EventArgs e)
        {
            Detail = new Views.UserProfile.UserInformationPage();
            IsPresented = false;

        }
        void Settings_Tapped(object sender, EventArgs e)
        {
            Detail = new Views.UserProfile.SettingsPage();
            IsPresented = false;

        }
    }
}
