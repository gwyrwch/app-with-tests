using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace test132132.ViewModels.Editor
{
    public class MultipleChoiceQViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Variant> Variants { get; set;}

        public MultipleChoiceQViewModel()
        {
            Variants = new ObservableCollection<Models.Variant>();
        }

        public void SetListLength(int length) 
        {
            while (Variants.Count > length)
            {
                Variants.RemoveAt(Variants.Count - 1);
            }

            while (Variants.Count < length)
            {
                Variants.Add(new Models.Variant("", false));
            }
        }
    }
}
