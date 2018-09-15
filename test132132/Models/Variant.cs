namespace test132132.Models
{
    public class Variant
    {
        public Variant(string answer, bool istrue)
        {
            Answer = answer;
            IsTrue = istrue;
        }
        public bool IsTrue { get; }
        public string Answer { get; }

        public override string ToString()
        {
            return Answer;
        }
    }
}
