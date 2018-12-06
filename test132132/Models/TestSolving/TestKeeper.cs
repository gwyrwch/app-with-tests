using System;
using test132132.Views.TestPreview;
using Xamarin.Forms;

namespace test132132.Models.TestSolving
{
    public static class TestKeeper
    {
        public static Page NextPage(Test test, int curQ, TestResults testResults)
        { 
            if (test.Count == curQ + 1)
                return new ResultsPage(test, testResults);

            Page nextPage = null; 

            if (test[curQ + 1].QType == 3)
                nextPage = new OpenQuestionPage(test, curQ + 1, testResults);
            if (test[curQ + 1].QType == 2)
                nextPage = new MatchingQuestionPage(test, curQ + 1, testResults);
            if (test[curQ + 1].QType == 1)
                nextPage = new MultipleChoiceQuestionPage(test, curQ + 1, testResults);

            NavigationPage.SetHasBackButton(nextPage, false);
            return nextPage;
        }
    }
}
