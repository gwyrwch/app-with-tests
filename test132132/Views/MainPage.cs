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


            if (!File.Exists(App.UserTestsPath))
                File.WriteAllText(App.UserTestsPath, "[]");

            // run tests


            Page itemsPage,  subjectsPage = null, editorPage = null;

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

                    itemsPage.Icon = "tab_feed.png";
                    subjectsPage.Icon = "tab_feed.png";
                    editorPage.Icon = "tab_about.png";
                    break;
            }

            Children.Add(subjectsPage);
            Children.Add(editorPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
