﻿using System;
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
            CurrentUser = App.CurrentUser;
        }

        public bool BirthdayToday()
        {
            if (CurrentUser.Birth.Day == DateTime.Today.Day &&
                 CurrentUser.Birth.Month == DateTime.Today.Month)
                return true;
            return false;
        }
    }
}
