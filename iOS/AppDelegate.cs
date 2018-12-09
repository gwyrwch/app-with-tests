using Foundation;
using UIKit;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.iOS;
using XLabs.Forms;
using Lottie.Forms.iOS.Renderers;

namespace test132132.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : XFormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();
            AnimationViewRenderer.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackOpaque, false);
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
