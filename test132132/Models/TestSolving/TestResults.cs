using System;
namespace test132132.Models.TestSolving
{
    public class TestResults
    {
        public int CorrectAnswers { get; set; }
        public TimeSpan UsedTime { get; set; }
        public int TotalPoints { get; set; }

        public void Update(Question question, TimeSpan usedTime, bool correct)
        {
            UsedTime += usedTime;
            if (correct)
            {
                CorrectAnswers += 1;
                TotalPoints += question.Points.Value;
            }
        }
    }
}
