﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class EditorPage : ContentPage
    {

        public EditorPage()
        {
            InitializeComponent();
            SubjectPicker.ItemsSource = App.Subjects;
            TimeModePicker.ItemsSource = Common.Mappers.AvaliableTimeModes();
        }
    }
}
