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
                QuestionTextEntry.Text,
                Common.MyInt.Parse(PointsEntry.Text),
                QuestionAnswerEntry.Text
            );

            try
            {
                question.Validate();
            }
            catch (Exception exc)
            {
                await DisplayAlert(iOS.AppResources.MatchingQEditPageInvalidQuestion, exc.Message, iOS.AppResources.CommonOk);
                return;
            }

            MessagingCenter.Send(
                (ContentPage)this,
                "CreateNewQuestion",
                (Models.Question)question
            );
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage()); // todo
        }

    }
}
