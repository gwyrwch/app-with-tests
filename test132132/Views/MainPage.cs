using System;
using System.IO;

using Xamarin.Forms;
using test132132.Views;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace test132132
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            UnitTests.InitUnitTests.Run();


            if (!File.Exists(App.userTestsPath))
                File.WriteAllText(App.userTestsPath, "[]");

            if (!File.Exists(App.lastUserPath))
                File.WriteAllText(App.lastUserPath, "");

            // run tests


            Page itemsPage, subjectsPage = null, editorPage = null, profilePage = null;

            Page authPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = ""
                    };
                    subjectsPage = new NavigationPage(new SubjectsPage())
                    {
                        Title = "Tests"
                    };
                    editorPage = new NavigationPage(new EditorTestsPage())
                    {
                        Title = "Editor"
                    };
                    profilePage = new NavigationPage(new ProfilePage())
                    {
                        Title = "Profile"
                    };
                    authPage = new NavigationPage(new Views.UserProfile.Authorization.AuthorizationPage());
                    authPage.Title = "Profile";
                    // authPage.Icon = "tempIcon.jpeg";

                    authPage.Icon = "tab_feed.png";
                    itemsPage.Icon = "tab_feed.png";
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

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
