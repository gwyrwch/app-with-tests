using System;

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
                Common.MyInt.Parse(Points.Text),
                QuestionAnswer.Text
            );

            try {
                question.Validate();
            } catch (Exception exc) {
                await DisplayAlert("Question is invalid", exc.Message, "Ok");
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
