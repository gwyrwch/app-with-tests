using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace test132132
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public static ObservableCollection<string> Subjects = new ObservableCollection<string> {
            //"History",
            //"Geography",
            //"Maths",
            "C#",
            "C++",
           // "C",
            "Assembler"
            //"Physics"
        };

        public static string userTestsPath = "./userTests.json";
        public static string lastUserPath = "./lastUser.txt";

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

            // ./gwyrwch.json
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
            //string lastUserFromFile = File.ReadAllText(App.lastUserPath);
            //if (lastUserFromFile == string.Empty)
            // return new Models.User();

            // ./gwyrwch.json

            //JsonConvert.DeserializeObject<Models.User>(plainText);

            //CurrentUser = newUser;
            File.WriteAllText(
                fileName,
                JsonConvert.SerializeObject(newUser)
            );
        }

        public static ObservableCollection<Models.Test> CreateSomeTestsTemp()
        {
            Models.Test questions = new Models.Test();
            questions.Title = "Sample C# test";
            questions.Subject = Subjects[Subjects.IndexOf("C#")];
            questions.TimeLimit = TimeSpan.FromSeconds(30);
            questions.Mode = Models.TimeMode.limitForQuestion;

            questions.Add(new Models.MultipleChoiceQuestion(
                "Выберите верные утверждения :",
                10,
                new List<Models.Variant>
                {
                    new Models.Variant("Метод Main может быть ассинхронным", true),
                    new Models.Variant("Свойстро Id класса Task задается задаче при ее создании"),
                    new Models.Variant("Метод Task.WaitAny() имеет тип возвращаемого значения void"),
                    new Models.Variant("свойство CreationOptions в классе Task является перечислением", true)
                }
            ));

            questions.Add(new Models.OpenQuestion(
                "Как называется свойство, значение которого будет установлено, если привзяка" +
                "прошла успешно, но установлено значение null?",
                10,
                "TargetNullValue"
            ));

            questions.Add(new Models.MatchingQuestion(
                "Соотнесите утверждения, связанные со свойством Mode класса Binding:",
                10,
                new List<string>
                {
                    "OneWay",
                    "TwoWay",
                    "OneTime"
                },
                new List<string>
                {
                    "Данные идут в обоих направлениях между источником и целью",
                    "Данные идут от источника к цели",
                    "Данные идут от источника к цели, но только если изманился BindingContext"
                },
                new List<int?>{2, 1, 3}
            ));

            return new ObservableCollection<Models.Test>{questions};
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

        public App()
        {
            InitializeComponent();

            TempUser();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());


        }
    }
}
