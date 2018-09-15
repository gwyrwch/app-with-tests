using System;

using Xamarin.Forms;
using test132132.Views;

namespace test132132
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, aboutPage = null, subjectsPage = null, editorPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = ""
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };

                    subjectsPage = new NavigationPage(new SubjectsPage())
                    {
                        Title = "Tests"
                    };
                    editorPage = new NavigationPage(new EditorPage())
                    {
                        Title = "Editor"
                    };

                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    subjectsPage.Icon = "tab_feed.png";
                    editorPage.Icon = "tab_about.png";

                    editorPage.BackgroundColor = Color.Cyan;

                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Browse"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };

                    subjectsPage = new SubjectsPage();
                    break;
            }

            //Children.Add(itemsPage);
            Children.Add(subjectsPage);
            Children.Add(editorPage);
            //Children.Add(aboutPage);
            //Children.Add(new ItemsPage());
            //Children.Add(new SubjectsPage());

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
