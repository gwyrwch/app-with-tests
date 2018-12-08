using System;
using System.Collections.Generic;

namespace test132132.Models
{
    public class UserStatistics
    {
        public List<int> SolvedTests { get; set; }
        public List<int> Points { get; set; }
        public int CorrectAnswers { get; set; }

        public UserStatistics()
        {
            SolvedTests = new List<int>();
            Points = new List<int>();
        }

        public void UpdateWith(Test test, TestSolving.TestResults testResults)
        {
            SolvedTests.Add(test.Id);
            Points.Add(testResults.TotalPoints);
            CorrectAnswers += testResults.CorrectAnswers;
        }
    }
}
