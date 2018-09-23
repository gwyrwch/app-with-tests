using System;
using Xamarin.Forms;

namespace test132132.Common
{
    public class TimeCountdownPicker : Picker
    {
        public static readonly BindableProperty SelectedTimeProperty =
            BindableProperty.Create(
                nameof(SelectedTime), 
                typeof(TimeSpan), 
                typeof(TimeCountdownPicker), 
                defaultValue: TimeSpan.Zero, 
                defaultBindingMode: BindingMode.TwoWay, 
                propertyChanged: OnSelectedTimePropertyPropertyChanged
            );

        public TimeCountdownPicker()
        {
            // Add only one item, later will manipulate only it's value for performance
            Items.Add("00:00:00");
            SelectedIndex = 0;
            SelectedTime = TimeSpan.Zero;
        }

        public TimeSpan SelectedTime
        {
            get { return (TimeSpan)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        private static void OnSelectedTimePropertyPropertyChanged(
            BindableObject bindable, 
            object value, 
            object newValue
        )
        {
            var picker = (TimeCountdownPicker)bindable;
            // Update value
            picker.Items[0] = newValue.ToString();
            picker.SelectedIndex = 0;
        }
    }
}
