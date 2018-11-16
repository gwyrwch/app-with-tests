using System;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace test132132.ViewModels.User
{
    public class UserProfileViewModel : MyBaseViewModel
    {
        public Models.User CurrentUser { get; set; }
        public UserProfileViewModel()
        {
            CurrentUser = App.GetCurrentUser();
        }

       

    }
}
