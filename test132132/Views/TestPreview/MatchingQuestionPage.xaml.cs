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
        private int numCorrectAns;

        public MatchingQuestionPage()
        {
            InitializeComponent();
        }

        public MatchingQuestionPage(Test test, int v, int numCorrectAns)
        {
            this.test = test;
            this.v = v;
            this.numCorrectAns = numCorrectAns;
        }
    }
}
