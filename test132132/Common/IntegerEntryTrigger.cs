using System;
using Xamarin.Forms;

namespace test132132.Common
{
    public class IntegerEntryTrigger : TriggerAction<Entry>
    {
        private string _prevValue = string.Empty;

        protected override void Invoke(Entry entry)
        {
            var isNumeric = int.TryParse(entry.Text, out int n);

            if (!string.IsNullOrWhiteSpace(entry.Text) && (entry.Text.Length > 3 || !isNumeric))
            {
                entry.Text = _prevValue;
                return;
            }

            _prevValue = entry.Text;
        }
    }
}

