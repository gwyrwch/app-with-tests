using System.IO;

using Xamarin.Forms;
using test132132.Views;

namespace test132132
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            BarBackgroundColor = (Color)Application.Current.Resources["BottomBarBackgroundColor"];
            BarTextColor = (Color)Application.Current.Resources["BarTextColor"];

            MessagingCenter.Subscribe<Views.UserProfile.SettingsPage>(this, "ChangeColorBottomBar",
                (obj) => {
                    ChangeBottomBar();
                }
            );
            MessagingCenter.Subscribe<Views.UserProfile.SettingsPage>(this, "RenderingAsked",
                (obj) => {
                    UpdateAllChildren();
                }
            );


            UnitTests.InitUnitTests.Run();

            if (!File.Exists(App.userTestsPath))
                File.WriteAllText(App.userTestsPath, "[]");

            Page subjectsPage = null, editorPage = null;

            Page authPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    subjectsPage = new NavigationPage(new SubjectsPage())
                    {
                        Title = iOS.AppResources.MainPageTests
                    };
                    editorPage = new NavigationPage(new EditorTestsPage())
                    {
                        Title = iOS.AppResources.MainPageEditor
                    };
                    authPage = new NavigationPage(
                        new Views.UserProfile.Authorization.AuthorizationPage()
                    )
                    {
                        Title = iOS.AppResources.MainPageProfile,
                        Icon = "tab_feed.png"
                    };
                    //subjectsPage.Icon = "tab_feed.png";
                    //editorPage.Icon = "tab_feed.png";
                    subjectsPage.Icon = "tab_feed.png";
                    editorPage.Icon = "tab_feed.png";
                    //profilePage.Icon = "tttt.svg";
                    break;
            }

            Children.Add(authPage);
            Children.Add(editorPage);
            Children.Add(subjectsPage);

            Title = Children[0].Title;
        }

        async void UpdateAllChildren()
        {
            await Navigation.PushModalAsync(new LoadingPage());
            Children.Clear();

            Page subjectsPage = null, editorPage = null;
            Page profilePage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    subjectsPage = new NavigationPage(new SubjectsPage())
                    {
                        Title = iOS.AppResources.MainPageTests
                    };
                    editorPage = new NavigationPage(new EditorTestsPage())
                    {
                        Title = iOS.AppResources.MainPageEditor
                    };
                    profilePage = new NavigationPage(
                        new ProfilePage { Detail = new Views.UserProfile.SettingsPage() }
                    )
                    {
                        Title = iOS.AppResources.MainPageProfile,
                        Icon = "tab_feed.png"
                    };
                    subjectsPage.Icon = "tab_feed.png";
                    editorPage.Icon = "tab_feed.png";
                    break;
            }

            Children.Add(profilePage);
            Children.Add(editorPage);
            Children.Add(subjectsPage);
            Children.Add(new NavigationPage(new Views.UserProfile.BirthdayPage()));

            await Navigation.PopModalAsync();
        }

        void ChangeBottomBar()
        {
            BarBackgroundColor = (Color)Application.Current.Resources["BottomBarBackgroundColor"];
            BarTextColor = (Color)Application.Current.Resources["BarTextColor"];
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
