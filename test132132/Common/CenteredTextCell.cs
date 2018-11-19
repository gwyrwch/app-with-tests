using System;
using Xamarin.Forms;

namespace test132132.Common
{
    public class CenteredTextCell : ViewCell
    {
        public CenteredTextCell(string text, Color color)
        {
            var label = new Label
            {
                TextColor = color,
                Text = text,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };

            var stacklayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(10, 0, 10, 0),
                HorizontalOptions = LayoutOptions.Center,
                Children = { label }
            };


            View = stacklayout;
        }
    }
}
