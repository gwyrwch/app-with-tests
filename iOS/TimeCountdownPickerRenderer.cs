using System;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using test132132.Common;
using test132132.iOS;

[assembly: ExportRendererAttribute(typeof(TimeCountdownPicker), typeof(TimeCountdownPickerRenderer))]
namespace test132132.iOS
{
    public class TimeCountdownPickerRenderer : PickerRenderer
    {
        public static double DisplayScale = UIScreen.MainScreen.Scale;
        public static int DispalyHeight = (int)UIScreen.MainScreen.NativeBounds.Height;
        public static int DisplayWidth = (int)UIScreen.MainScreen.NativeBounds.Width;

        internal const int ComponentCount = 6;

        private const int _labelSize = 30;

        private TimeCountdownPicker timeCountdownPicker;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

                timeCountdownPicker = e.NewElement as TimeCountdownPicker;

                var customModelPickerView = new UIPickerView
                {
                    Model = new TimeCountdownPickerView(timeCountdownPicker)
                };

                SelectPickerValue(customModelPickerView, timeCountdownPicker);

                CreatePickerLabels(customModelPickerView);

                Control.InputView = customModelPickerView;
            }
        }

        private void SelectPickerValue(UIPickerView customModelPickerView, TimeCountdownPicker myTimePicker)
        {
            if (myTimePicker == null)
                return;
            customModelPickerView.Select(new nint(myTimePicker.SelectedTime.Hours), 0, false);
            customModelPickerView.Select(new nint(myTimePicker.SelectedTime.Minutes), 2, false);
            customModelPickerView.Select(new nint(myTimePicker.SelectedTime.Seconds), 4, false);
        }

        private void CreatePickerLabels(UIPickerView customModelPickerView)
        {
            nfloat verticalPosition = (customModelPickerView.Frame.Size.Height / 2) - (_labelSize / 2);
            nfloat componentWidth = new nfloat(DisplayWidth / ComponentCount / DisplayScale);

            var hoursLabel = new UILabel(new CGRect(componentWidth, verticalPosition, _labelSize, _labelSize));
            hoursLabel.Text = "hours";

            var minutesLabel = new UILabel(new CGRect((componentWidth * 3) + (componentWidth / 2), verticalPosition, _labelSize, _labelSize));
            minutesLabel.Text = "min";

            var secondsLabel = new UILabel(new CGRect((componentWidth * 5) + (componentWidth / 2), verticalPosition, _labelSize, _labelSize));
            secondsLabel.Text = "sec";

            customModelPickerView.AddSubview(hoursLabel);
            customModelPickerView.AddSubview(minutesLabel);
            customModelPickerView.AddSubview(secondsLabel);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
            {
                var customModelPickerView = (UIPickerView)Control.InputView;

                SelectPickerValue(customModelPickerView, timeCountdownPicker);
            }
        }

        public class TimeCountdownPickerView : UIPickerViewModel
        {
            private TimeCountdownPicker timeCountdownPicker { get; }

            public TimeCountdownPickerView(TimeCountdownPicker picker)
            {
                timeCountdownPicker = picker;
            }

            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return new nint(ComponentCount);
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                if (component == 0)
                {
                    // Hours
                    return new nint(24);
                }

                if (component % 2 != 0)
                {
                    // Odd components are labels for hrs, mins and secs
                    return new nint(1);
                }

                // Minutes & seconds
                return new nint(60);
            }

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                if (component == 0)
                {
                    return row.ToString();
                }
                else if (component == 1)
                {
                    return null;
                }
                else if (component == 3)
                {
                    return null;
                }
                else if (component == 5)
                {
                    return null;
                }
                return row.ToString("#0");
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                var selectedHours = pickerView.SelectedRowInComponent(0);
                var selectedMinutes = pickerView.SelectedRowInComponent(2);
                var selectedSeconds = pickerView.SelectedRowInComponent(4);

                var time = new TimeSpan((int)selectedHours, (int)selectedMinutes, (int)selectedSeconds);

                timeCountdownPicker.SelectedTime = time;
            }

            public override nfloat GetComponentWidth(UIPickerView pickerView, nint component)
            {
                var screenWidth = DisplayWidth;

                var componentWidth = screenWidth /
                    ComponentCount /
                    DisplayScale;

                return new nfloat(componentWidth);
            }
        }
    }
}