using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class EditorPage : ContentPage
    {
        Models.Test test = new Models.Test();
       // Models.Test test;
        bool testCreated = false;

        public EditorPage()
        {
            InitializeComponent();
            SubjectPicker.ItemsSource = App.Subjects;
            TimeModePicker.ItemsSource = Common.Mappers.AvaliableTimeModes();

            MessagingCenter.Subscribe<ContentPage, Models.Question>(
                this, "CreateNewQuestion", (obj, question) =>
                {
                    test.Add(question);
                }
            );

        }

        async void AddQuestion_Clicked(object sender, EventArgs e) 
        {
            //if(!testCreated) {
            //    test.TimeLimit = (TimeSpan?)TimeLimitPicker.SelectedItem;
            //    test.Mode = (Models.TimeMode?)TimeModePicker.SelectedItem;

            //    testCreated = true;
            //}

            try {
                if (
                    string.IsNullOrWhiteSpace(TitleEntry.Text) ||
                    SubjectPicker.SelectedIndex == -1 ||
                    TimeLimitPicker.SelectedIndex == -1 ||
                    TimeModePicker.SelectedIndex == -1
                ){
                    throw new NullReferenceException("Error! There are empty fields.");
                }
            }
            catch(Exception exc)
            {
                await DisplayAlert("Data is invalid", exc.Message, "Ok");
                return;
            }

            if(!testCreated) 
            {
                test.Title = TitleEntry.Text;
                test.Subject = SubjectPicker.SelectedItem.ToString();
                test.TimeLimit = TimeLimitPicker.SelectedTime;
                test.Mode = Models.TimeMode.limitForQuestion; //todo
                //test.Mode = (Models.TimeMode)TimeModePicker.SelectedItem; todo

                TitleEntry.IsEnabled = false;
                SubjectPicker.IsEnabled = false;
                TimeLimitPicker.IsEnabled = false;
                TimeModePicker.IsEnabled = false;

                testCreated = true;
            }

            await Navigation.PushAsync(new QTypeSelectionPage());
        }
    }
}
