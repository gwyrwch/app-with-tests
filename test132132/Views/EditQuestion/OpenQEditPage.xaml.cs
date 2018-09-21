﻿using System;

using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132
{
    public partial class OpenQEditPage : ContentPage
    {
        public OpenQEditPage()
        {
            InitializeComponent();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Models.OpenQuestion question = new Models.OpenQuestion(
                QuestionText.Text,
                int.Parse(Points.Text),
                QuestionAnswer.Text
            );
            MessagingCenter.Send(this, "CreateNewOpenQuestion", question);
            await Navigation.PopToRootAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage()); // todo
        }

    }
}
