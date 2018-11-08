using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace test132132.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackOpaque, false);
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
