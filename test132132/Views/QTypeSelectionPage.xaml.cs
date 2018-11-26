using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132
{
    public partial class QTypeSelectionPage : ContentPage
    {
        Page nextPage;
        Page multipleChoicePage, matchingPage, openPage;

        public QTypeSelectionPage()
        {
            InitializeComponent();
            nextPage = null;
            multipleChoicePage = new MultipleChoiceQEditPage();
            matchingPage = new MatchingQEditPage();
            openPage = new OpenQEditPage();
        }

        async void QEditPage_Clicked(object sender, EventArgs e) //next clicked 
        {
            if (nextPage == null) {
                await DisplayAlert("Try again.", "Please, select the type.", "Ok");
                return;
            }
            await Navigation.PushAsync(nextPage);
        }

        void RecolorButtons(Button selectedButton) { //chaange the color of the selected
            MultipleChoiceButton.TextColor =
            MatchingButton.TextColor =
            OpenQuestionButton.TextColor =
                                             (Color)Application.Current.Resources["MainGreyColor"];

            MultipleChoiceButton.BorderColor = 
            MatchingButton.BorderColor       = 
            OpenQuestionButton.BorderColor   =
                                               (Color)Application.Current.Resources["Silver"];
                        
            selectedButton.TextColor = Color.FromHex("#5ac8fa");
            selectedButton.BorderColor = Color.FromHex("#5ac8fa");
        }

        void MultipleChoiceQ_Clicked(object sender, EventArgs e)
        {
            RecolorButtons(sender as Button);
            nextPage = multipleChoicePage;
        }

        void MatchingQ_Clicked(object sender, EventArgs e)
        {
            RecolorButtons(sender as Button);
            nextPage = matchingPage;
        }

        void OpenQ_Clicked(object sender, EventArgs e)
        {
            RecolorButtons(sender as Button);
            nextPage = openPage;
        }
    }
}
