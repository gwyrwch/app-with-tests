using System;
using System.IO;

using Xamarin.Forms;
using test132132.Views;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using UIKit;
using test132132.Views.TestPreview;
using System.Collections.Generic;

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
            //Children.Add(new NavigationPage(new MultipleChoiceQuestionPage(
            //    new Models.Test("kek", "mda", TimeSpan.FromSeconds(10), Models.TimeMode.limitForQuestion) { 
            //        new Models.MultipleChoiceQuestion(
            //            "123", 123, new
            //            List<Models.Variant> { 
            //                new Models.Variant("1", true), new Models.Variant("2", false) 
            //            } )
            //    },
            //    0
            //    ,
            //    new Models.TestSolving.TestResults
            //    {
            //        CorrectAnswers = 0, TotalPoints = 0, UsedTime = TimeSpan.FromSeconds(0)
            //    }

            //)));



            Children.Add(new NavigationPage(new MatchingQuestionPage(
                new Models.Test("kek", "mda", TimeSpan.FromSeconds(60), Models.TimeMode.limitForQuestion) {
                    new Models.MatchingQuestion("123", 12,
                        new List<string> {"1", "2", "3"},
                        new List<string> {"c", "a", "b"},
                        new List<int?> {1, 2, 0}
                    )
                },
                0,
                new Models.TestSolving.TestResults
                {
                    CorrectAnswers = 0,
                    TotalPoints = 0,
                    UsedTime = TimeSpan.FromSeconds(0)
                }

            )));

            Children.Add(new NavigationPage(new TestChosedPage(
                new Models.Test("kek", "mda", TimeSpan.FromSeconds(4005), Models.TimeMode.limitForQuestion) {
                    new Models.MatchingQuestion("123", 12,
                        new List<string> {"1", "2", "3"},
                        new List<string> {"c", "a", "b"},
                        new List<int?> {1, 2, 0}
                    )
                }
            )));


            //Children.Add(new NavigationPage(new OpenQuestionPage(
            //    new Models.Test("kek", "mda", TimeSpan.FromSeconds(30), Models.TimeMode.limitForTest) {
            //        new Models.OpenQuestion("Blaa?", 12, "bla"),
            //        new Models.OpenQuestion("Keks?", 12, "kek")
            //    },
            //    0,
            //    new Models.TestSolving.TestResults
            //    {
            //        CorrectAnswers = 0,
            //        TotalPoints = 0,
            //        UsedTime = TimeSpan.FromSeconds(0)
            //    }
            //)));

            Title = Children[0].Title;
        }

        void UpdateAllChildren()
        {
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
                        new ProfilePage() { Detail = new Views.UserProfile.SettingsPage() }
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
