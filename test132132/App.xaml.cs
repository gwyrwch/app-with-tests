using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace test132132
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public static ObservableCollection<string> Subjects = new ObservableCollection<string> {
            "History", 
            "Chemistry",
            "Maths"
        };

        public App()
        {
            InitializeComponent();

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
