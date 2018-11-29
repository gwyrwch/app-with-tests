using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.iOS;

namespace test132132.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackOpaque, false);
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(118,53,235);

            //MessagingCenter.Subscribe<Views.UserProfile.SettingsPage>(this, "ChangeColorBottomBar",
            //    (obj) => {
            //        ChangeBottomBar();
            //    }
            //);

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        //private void ChangeBottomBar()
        //{
        //    UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(118, 53, 235);
        //}
    }
}
