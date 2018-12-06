using System;
namespace test132132.Models.TestSolving
{
    public class TestResults
    {
        public int CorrectAnswers { get; set; } = 0;
        public TimeSpan UsedTime { get; set; } = TimeSpan.FromSeconds(0);
        public int TotalPoints { get; set; } = 0;

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
