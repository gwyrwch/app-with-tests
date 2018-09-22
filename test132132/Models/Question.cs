namespace test132132.Models
{
    public abstract class Question
    {
        protected Question(string text, int? points)
        {
            Text = text;
            Points = points;
            Test = null;
        }
        public string Text { get; }
        public int? Points { get; }
        public Test Test { get; set; }

        public abstract void Validate();
    }
}
