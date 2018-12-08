using System;
using Xamarin.Forms;

namespace test132132.Common
{
    public partial class CustomTableView : TableView
    {
        public static BindableProperty GroupHeaderColorProperty = 
            BindableProperty.Create(
                "GroupHeaderColor", 
                typeof(Color), 
                typeof(CustomTableView), 
                Color.White
            );
        public Color GroupHeaderColor
        {
            get
            {
                return (Color)GetValue(GroupHeaderColorProperty);
            }
            set
            {
                SetValue(GroupHeaderColorProperty, value);
            }
        }

        public CustomTableView()
        {
            InitializeComponent();
        }
    }
}
