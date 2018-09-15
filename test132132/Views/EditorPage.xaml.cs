using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132
{
    public partial class EditorPage : ContentPage
    {
        public EditorPage()
        {
            InitializeComponent();
            SubjectPicker.ItemsSource = App.Subjects;
        }
    }
}
