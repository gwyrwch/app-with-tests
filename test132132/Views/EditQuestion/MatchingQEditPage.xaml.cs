using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class MatchingQEditPage : ContentPage
    {
        ViewModels.Editor.MatchingQViewModel viewModel;
        public MatchingQEditPage()
        {
            BindingContext = viewModel = new ViewModels.Editor.MatchingQViewModel();

            InitializeComponent();

            NumberOfAnswersPicker.ItemsSource = Enumerable.Range(1, 10).ToList();
        }

        void AmountSelected(object sender, EventArgs e)
        {
            int length = (int)NumberOfAnswersPicker.SelectedItem;
            viewModel.SetListLength(length);
        }

        void OnLeftsSelected(object sender, SelectedItemChangedEventArgs args)
        {
            LeftsListView.SelectedItem = null;
        }

        void OnRightsSelected(object sender, SelectedItemChangedEventArgs args)
        {
            RightsListView.SelectedItem = null;
        }

        public async void Save_Clicked(object sender, EventArgs e)
        {
            if (!viewModel.Shuffled) {
                bool responce = await DisplayAlert(
                    iOS.AppResources.MatchingQEditPageForgotToShuffle, 
                    "", 
                    iOS.AppResources.CommonOk,
                    iOS.AppResources.CommonCancel
                );
                if (responce == false)
                    return;
            }

            Models.MatchingQuestion question = new Models.MatchingQuestion(
                QuestionTextEntry.Text,
                Common.MyInt.Parse(PointsEntry.Text),
                viewModel.LeftsAsList(),
                viewModel.RightsAsList(),
                viewModel.Relation.ToList()
            );

            try
            {
                question.Validate();
            } catch(Exception exc) {
                await DisplayAlert(
                    iOS.AppResources.MatchingQEditPageInvalidQuestion, 
                    exc.Message, 
                    iOS.AppResources.CommonOk
                );
                return;
            }

            MessagingCenter.Send(
                (ContentPage)this,
                "CreateNewQuestion",
                (Models.Question)question
            );

            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync()); //todo
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage()); // todo
        }

        async void OnShuffle_Clicked(object sender, EventArgs e) 
        {
            if (!viewModel.Shuffled) {
                bool responce = await DisplayAlert(
                    iOS.AppResources.MatchingQEditPageStartShuffling,
                    iOS.AppResources.MatchingQEditPageOptionIsUnrecoverable,
                    iOS.AppResources.CommonOk, iOS.AppResources.CommonCancel
                );
                if (responce)
                {
                    viewModel.Shuffled = true;
                    //LeftsListView.InputTransparent = true;
                    //RightsListView.InputTransparent = true;
                    NumberOfAnswersPicker.InputTransparent = true;
                }
                else return;
            }

            viewModel.Shuffle();
         }
    }
}
