using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class EditorPage : ContentPage
    {

        public EditorPage()
        {
            InitializeComponent();
            SubjectPicker.ItemsSource = App.Subjects;
            TimeModePicker.ItemsSource = Common.Mappers.AvaliableTimeModes();
        }

        private async void AddQuestion_Clicked(object sender, EventArgs e) 
        {
            await Navigation.PushAsync(new QTypeSelectionPage());
        }
    }
}
