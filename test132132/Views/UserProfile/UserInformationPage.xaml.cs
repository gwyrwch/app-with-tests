using System;
using System.Collections.Generic;

using Xamarin.Forms;

using ImageCircle.Forms.Plugin.iOS;
using System.Threading;
using System.Threading.Tasks;

namespace test132132.Views.UserProfile
{
    public partial class UserInformationPage : ContentPage
    {
        ViewModels.User.UserProfileViewModel viewModel;
        static bool appearedOnce = false;
        public UserInformationPage()
        {
            InitializeComponent();

            viewModel = new ViewModels.User.UserProfileViewModel();
            Information.BindingContext = viewModel.CurrentUser;
            
            BirthLabel.Text = string.Join(" ", viewModel.CurrentUser.GetUserAge().ToString(), "years old"); //todo translate
        }

        async void StartBirthAnimation(object s, EventArgs e)
        {
            if(viewModel.BirthdayToday() && !appearedOnce)
            {
                await Navigation.PushModalAsync(new Views.UserProfile.BirthdayPage());  //todo long d,aoas;ldklaksjdlkajsdwqpokdlaksndl#include <iostream>
                
                appearedOnce = true;

                await Task.Run(
                    () => Thread.Sleep(3000)
                );

                await Navigation.PopModalAsync();
            }
        }

        async void Statistics_Tapped(object s, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticsPage(viewModel.CurrentUser.Stats));
        }


        async void Rating_Tapped(object s, EventArgs e)
        {
            await Navigation.PushAsync(new RatingPage());
        }
    }
}
