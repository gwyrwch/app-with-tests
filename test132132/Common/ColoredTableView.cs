using System;
using Xamarin.Forms;

namespace test132132.Common
{
    public partial class ColoredTableView : TableView
    {
        public static BindableProperty SeparatorColorProperty = BindableProperty.Create("SeparatorColor", typeof(Color), typeof(ColoredTableView), Color.White);
        public Color SeparatorColor
        {
            get
            {
                return (Color)GetValue(SeparatorColorProperty);
            }
            set
            {
                SetValue(SeparatorColorProperty, value);
            }
        }

        //public static BindableProperty SectionBackgroundColorProperty = BindableProperty.Create("BackgroundColor", typeof(Color), typeof(ColoredTableView), Color.DarkRed);
        //public Color SectionBackgroundColor
        //{
        //    get
        //    {
        //        return (Color)GetValue(SectionBackgroundColorProperty);
        //    }
        //    set
        //    {
        //        SetValue(SectionBackgroundColorProperty, value);
        //    }
        //}


    }
}

