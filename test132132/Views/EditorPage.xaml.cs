﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class EditorPage : ContentPage
    {
        Models.Test test = new Models.Test();

        public EditorPage()
        {
            InitializeComponent();
            SubjectPicker.ItemsSource = App.Subjects;
            TimeModePicker.ItemsSource = Common.Mappers.AvaliableTimeModes();
           // NavigationPage.SetBackButtonTitle(this, "kkek"); //this work fixme

            MessagingCenter.Subscribe<ContentPage, Models.Question>(
                this, "CreateNewQuestion", (obj, question) =>
                {
                    AddQuestion(question);
                }
            );

            EditorImage.Source = App.EditorImagePath;

            MessagingCenter.Subscribe<Views.UserProfile.SettingsPage>(this, "ChangesAsked",
                (obj) => {
                    ImagePathChanged();
                    Render();
                }
            );
        }

        void Render()
        {
            var e = test.ToList();
            test.Clear(); 
            AddedQuestionsTableView.Root.First().Clear();
            foreach (var q in e)
            {
                AddQuestion(q);
            }
        }

        void AddQuestion(Models.Question question)
        {
            test.Add(question);

            StackLayout layout = new StackLayout
            { // manually adding question to table
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(15, 15, 0, 15)
            };
            layout.Children.Add(
                new Label
                {
                    Text = string.Join(
                        "",
                        AddedQuestionsTableView.Root.Count.ToString(),
                        "."
                    ),
                    TextColor = (Color)Application.Current.Resources["MainColor"]
                }
            );
            layout.Children.Add(
                new Label
                {
                    Text = question.Text,
                    TextColor = (Color)Application.Current.Resources["MainGreyColor"]
                }
            );
            AddedQuestionsTableView.Root.First().Add( // Root.First() -> first table section
                new ViewCell
                {
                    View = layout
                }
            );
        }


        void ImagePathChanged()
        {
            EditorImage.Source = App.EditorImagePath;
        }

        void Validate()
        {
            if (
                string.IsNullOrWhiteSpace(TitleEntry.Text) ||
                SubjectPicker.SelectedIndex == -1 ||
                TimeLimitPicker.SelectedIndex == -1 ||
                TimeModePicker.SelectedIndex == -1
            )
                throw new NullReferenceException("Error! There are empty fields.");
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                Validate();
            }
            catch (Exception exc)
            {
                await DisplayAlert("Data is invalid", exc.Message, "Ok");
                return;
            }

            test.Title = TitleEntry.Text;
            test.Subject = SubjectPicker.SelectedItem.ToString();
            test.TimeLimit = TimeLimitPicker.SelectedTime;
            test.Mode = (Models.TimeMode)TimeModePicker.SelectedIndex;

            MessagingCenter.Send( // todo
                this, "CreateNewTest", test
            );

            await Navigation.PopToRootAsync();
        }

        async void AddQuestion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage());
        }
    }
}
