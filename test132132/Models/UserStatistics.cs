using System;
using System.Collections.Generic;

namespace test132132.Models
{
    public class TestResults
    {
        public int SolvedQ { get; set; }
        public int TotalQ { get; set; }
        public string TestSubject { get; set; }
    }
    public class UserStatistics
    {
        public UserStatistics() 
        {
            SolvedTests = new List<TestResults>();
        }
        public List<TestResults> SolvedTests { get; set; }
        public int SummaryPoints { get; set; }
    }
}
