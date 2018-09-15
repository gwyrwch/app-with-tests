namespace test132132.Models
{
    public class Variant
    {
        public Variant(string answer, bool istrue)
        {
            Answer = answer;
            IsTrue = istrue;
        }
        public bool IsTrue { get; set; }

        public string Answer { get;
            set;        
        }

        public override string ToString()
        {
            return Answer;
        }
    }
}
