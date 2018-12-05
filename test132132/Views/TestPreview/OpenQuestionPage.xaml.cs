using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace test132132.Views.TestPreview
{
    public partial class OpenQuestionPage : ContentPage
    {
        Models.Test test;
        int curQ, numCorrectAns;
        public OpenQuestionPage(Models.Test test, int currentQuestion, int numberOfCorrectAnswers)
        {
            InitializeComponent();

            this.test = test;
            curQ = currentQuestion;
            numCorrectAns = numberOfCorrectAnswers;
            QuestionLabel.Text = string.Join("", 
                (currentQuestion + 1).ToString(), 
                ". ",
                test[currentQuestion].Text
            );
        }

        public async void Answer_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AnswerEntry.Text))
            {
                await DisplayAlert("", "", "ok");
                return;
            }

            if (AnswerEntry.Text.ToLower() == ((Models.OpenQuestion)test[curQ]).Answer.ToLower())
                numCorrectAns += 1;

            if (test.Count == curQ)
                return;
            //await Navigation.PushAsync(new NewItemPage(test, numCorrectAns)); //todo
            else if (test[curQ + 1].QType == 3)
                await Navigation.PushAsync(new OpenQuestionPage(test, curQ + 1, numCorrectAns));
            else if (test[curQ + 1].QType == 2)
                await Navigation.PushAsync(new MatchingQuestionPage(test, curQ + 1, numCorrectAns));
            else if (test[curQ + 1].QType == 1)
                await Navigation.PushAsync(new MultipleChoiceQuestionPage(test, curQ + 1, numCorrectAns));
        }
    }
}





//lw.ItemsSource = new[]
//{

//    new ObservableCollection<string> {"a", "b", "c"},
//    new ObservableCollection<string> {"a", "b", "c"}
//};