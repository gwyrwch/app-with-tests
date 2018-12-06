using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132
{
    public partial class ProfilePage : MasterDetailPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            Detail = new Views.UserProfile.UserInformationPage();
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
