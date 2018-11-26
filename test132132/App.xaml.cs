using System;
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

        public static Services.Settings settings;

        public static ObservableCollection<string> Subjects = new ObservableCollection<string> {
            "C#",
            "C++",
           // "C",
            "Assembler"
        };

        public const string userTestsPath = "./userTests.json";
        public const string lastUserPath = "./lastUser.txt";
        public const string settingsPath = "./settings.json";

        public static void TempUser()
        {
            Models.User currentUser = new Models.User();
            currentUser.Name = "Danik";
            currentUser.Surname = "Melnichenka";
            currentUser.UserName = "melnick";
            currentUser.Birth = new DateTime(1999, 12, 24);
            currentUser.Education = "BSU";
            currentUser.Email = "melnick@gmail.com";

            string fileName = string.Join("",
                 "./",
                 currentUser.UserName,
                  ".json"
            );

            File.WriteAllText(
                fileName,
                JsonConvert.SerializeObject(currentUser)
            );

            File.WriteAllText(
                lastUserPath,
                currentUser.UserName
            );
        }

        public static Models.User GetCurrentUser() 
        {
            string lastUserFromFile = File.ReadAllText(App.lastUserPath);
            if (lastUserFromFile == string.Empty)
                return new Models.User();

            string fileName = string.Join("",
                 "./",  
                  lastUserFromFile,
                  ".json"
            );
            string plainText = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Models.User>(plainText);                          
        }

        public static void UserChanged(Models.User newUser)
        {
            string fileName = string.Join("",
                 "./",
                 newUser.UserName,
                  ".json"
            );
            File.WriteAllText(
                lastUserPath,
                newUser.UserName
            );
            File.WriteAllText(
                fileName,
                JsonConvert.SerializeObject(newUser)
            );
        }

        protected override void OnSleep()
        {
            Models.User currentUser = GetCurrentUser();

            File.WriteAllText(lastUserPath, currentUser.UserName);
            File.WriteAllText(
                string.Join(
                    "", 
                    "./", 
                    currentUser.UserName, 
                    ".json"
                ),
                JsonConvert.SerializeObject(currentUser)
            );
        }

        public static void SetLanguage(string language)
        {
            CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList().First(element => element.EnglishName.Contains(language));
            iOS.AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
        }

        static void DefaultSettings()
        {
            Services.Settings defaultSettings = new Services.Settings { 
                Language = "English", 
                Theme = new Services.Theme { 
                    BackgroundColor = Color.Red 
                } 
            };

            File.WriteAllText(
                settingsPath,
                JsonConvert.SerializeObject(defaultSettings)
            );
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
            ApplySettings();
            MainImagePath = "GreyBlue.png";
            InitializeComponent();

            TempUser();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());
        }
    }
}
