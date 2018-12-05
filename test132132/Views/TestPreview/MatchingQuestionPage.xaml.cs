using System;
using System.Collections.Generic;
using test132132.Models;
using Xamarin.Forms;

namespace test132132.Views.TestPreview
{
    public partial class MatchingQuestionPage : ContentPage
    {
        private Test test;
        private int v;

        public MatchingQuestionPage(Test test, int v, Models.TestSolving.TestResults testResults)
        {
            this.test = test;
            this.v = v;


            InitializeComponent();
        }
    }
}
