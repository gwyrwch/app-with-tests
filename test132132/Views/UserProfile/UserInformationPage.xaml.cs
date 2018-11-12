using System;
using System.Collections.Generic;

using Xamarin.Forms;

using ImageCircle.Forms.Plugin.iOS;


namespace test132132.Views.UserProfile
{
    public partial class UserInformationPage : ContentPage
    {
        ViewModels.User.UserProfileViewModel viewModel;
        bool MessageCame = false;
        public UserInformationPage()
        {
            InitializeComponent();
            if (!MessageCame)
            {
                BindingContext = viewModel = new ViewModels.User.UserProfileViewModel();
                Information.BindingContext = viewModel.CurrentUser;

            }
            MessageCame = false;
            //BirthDayViewCell.BindingContext = viewModel.currentUser;
            //EducationViewCell.BindingContext = viewModel.currentUser;

            //MessagingCenter.Subscribe<ContentPage, Models.User>(
               //this, "EmailChanged", (obj, userWithNewEmail) =>
               //{
               //    viewModel = new ViewModels.User.UserProfileViewModel();
               //    viewModel.currentUser.Email = userWithNewEmail.Email;
               //    EmailLabel.BindingContext = viewModel.currentUser;
               //    //MessageCame = true; //todo doesn't work
               //});
        }
    }
}
