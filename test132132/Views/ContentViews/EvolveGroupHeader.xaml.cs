using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace test132132.Views.ContentViews
{
    public class EvolveGroupHeader : ViewCell
    {
        public EvolveGroupHeader()
        {
            View = new EvolveGroupHeaderView();
        }
    }

    public partial class EvolveGroupHeaderView : ContentView
    {
        public EvolveGroupHeaderView()
        {
            InitializeComponent();
        }
    }
}
