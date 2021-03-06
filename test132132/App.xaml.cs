﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Plugin.Multilingual;
using Xamarin.Forms;

namespace test132132
{
    public partial class App : Application
    {
        public static string MainImagePath { get; set; }
        public static string EditorImagePath { get; set; }

        public static Services.Settings settings;

        public static ObservableCollection<string> Subjects;

        static ObservableCollection<string> Subjects_En = new ObservableCollection<string> {
            "C#",
            "C++",
            "Assembler"
        };

        static ObservableCollection<string> Subjects_Ru = new ObservableCollection<string> {
            "C#",
            "C++",
            "Ассемблер"
        };

        public const string userTestsPath = "./userTests.json";
        public const string settingsPath = "./settings.json";

        public static Models.User CurrentUser { get; set; }

        protected override void OnSleep()
        {
            if (CurrentUser != null)
            {
                Common.UserBase.UpdateUser(CurrentUser);
            }
        }

        public static void SetLanguage(string language)
        {
            if (language == "Russian")
                Subjects = Subjects_Ru;
            else
                Subjects = Subjects_En;

            CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList(
            ).First(
                element => element.EnglishName.Contains(language)
            );

            iOS.AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
        }

        static void DefaultSettings()
        {
            Services.Settings defaultSettings = new Services.Settings { 
                Language = "Russian", 
                Theme = "DarkTheme"
            };

            File.WriteAllText(
                settingsPath,
                JsonConvert.SerializeObject(defaultSettings)
            );
        }

        public static void SetTheme(string theme)
        {
            if (theme == "LightTheme")
            {
                Current.Resources["Silver"] = Color.Silver;
                Current.Resources["MainColor"] = Color.FromHex("#007aff");
                Current.Resources["SomeLightBackgroundColor"] = Color.FromHex("#f9f9f9");
                Current.Resources["LightBackgroundColor"] = Color.FromHex("ffff");
                Current.Resources["MainGreyColor"] = Color.FromHex("#8e8e93");
                Current.Resources["BarBackgroundColor"] = Color.FromHex("#ecf0f1");
                Current.Resources["TextColor"] = Color.Black;
                Current.Resources["SeparatorColor"] = Color.Silver;
                Current.Resources["BottomBarBackgroundColor"] = Color.Default;
                Current.Resources["BarTextColor"] = Color.Default;
                Current.Resources["TableSectionColor"] = Color.FromHex("#ededed");

                MainImagePath = "GreyBlue.png";
                EditorImagePath = "Editor.png";
            } else if (theme == "DarkTheme")
            {
                Current.Resources["Silver"] = Color.FromHex("#fff2f5");
                Current.Resources["MainColor"] = Color.FromHex("#5ac8fa");
                Current.Resources["SomeLightBackgroundColor"] = Color.FromHex("#120D36");
                Current.Resources["LightBackgroundColor"] = Color.FromHex("#100b33");
                Current.Resources["MainGreyColor"] = Color.FromHex("#5ac8fa");
                Current.Resources["BarBackgroundColor"] = Color.FromHex("#131124");
                Current.Resources["TextColor"] = Color.WhiteSmoke;
                Current.Resources["SeparatorColor"] = Color.WhiteSmoke;
                Current.Resources["BottomBarBackgroundColor"] = Color.FromHex("#040026");
                Current.Resources["BarTextColor"] = Color.WhiteSmoke;
                Current.Resources["TableSectionColor"] = Color.FromHex("#131124");

                MainImagePath = "lightblue.png";
                EditorImagePath = "DarkThemeEditor.png";
            }
            else 
                throw new NotImplementedException();

            settings.Theme = theme;
            SaveSettings();
        }

        static void ApplySettings() 
        {
            string plainText;
            try
            {
                plainText = File.ReadAllText(
                    settingsPath
                );
            } catch (FileNotFoundException) {
                DefaultSettings();
                ApplySettings();
                return;
            }
            settings = JsonConvert.DeserializeObject<Services.Settings>(plainText);
            SetLanguage(settings.Language);
        }

        public static void SaveSettings()
        {
            File.WriteAllText(
                settingsPath,
                JsonConvert.SerializeObject(settings)
            );
        }

        public App()
        {
            MainImagePath = "GreyBlue.png";
            EditorImagePath = "Editor.png";

            ApplySettings();
            InitializeComponent();
            SetTheme(settings.Theme);

            Common.UserBase.CreateDanik();
            // dont know about current user at all
            CurrentUser = null;

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());

        }
    }
}
