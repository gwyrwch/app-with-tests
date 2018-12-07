using System;
using Xamarin.Forms;

namespace test132132.Models
{
    public class Variant: ICloneable, IEquatable<Variant>
    {
        public Variant(string answer, bool istrue = false) // done false
        {
            Answer = answer;
            IsTrue = istrue;
        }
        public bool IsTrue { 
            get; 
            set; 
        }

        public string Answer { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Variant other)
        {
            return IsTrue == other.IsTrue;
        }

        public override string ToString()
        {
            return Answer;
        }

        public double FontSize
        {
            get
            {
                if (Answer.Length >= 72)
                    return Device.GetNamedSize(NamedSize.Micro, typeof(Label));
                if (Answer.Length >= 42)
                    return Device.GetNamedSize(NamedSize.Small, typeof(Label));
                return Device.GetNamedSize(NamedSize.Default, typeof(Label));
            }
        }
    }
}
