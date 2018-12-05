using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using test132132.Models;
using Xamarin.Forms;

namespace test132132.Views.TestPreview
{
    public partial class MultipleChoiceQuestionPage : ContentPage
    {
        private Test test;
        private int curQ;

        ObservableCollection<Variant> Variants;

        public MultipleChoiceQuestionPage(Test test, int currentQuestion, Models.TestSolving.TestResults testResults)
        {
            this.test = test;
            this.curQ = currentQuestion;

            Variants = new ObservableCollection<Variant>(((MultipleChoiceQuestion)test[curQ]).Variants);

            InitializeComponent();
            AnswersListView.ItemsSource = Variants;

            QuestionLabel.Text = string.Join("",
                (curQ + 1).ToString(),
                ". ",
                test[curQ].Text
            );
        }


    }
}
