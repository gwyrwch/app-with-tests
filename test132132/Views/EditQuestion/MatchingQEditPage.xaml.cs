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
            InitializeComponent();
            NumberOfAnswersPicker.ItemsSource = Enumerable.Range(1, 10).ToList();

            BindingContext = viewModel = new ViewModels.Editor.MatchingQViewModel();
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
           // await Navigation.PushAsync(new MatchingQRelationPage())

            viewModel.SaveRights();
            viewModel.SaveLefts();

            Models.MatchingQuestion question = new Models.MatchingQuestion(
                QuestionTextEntry.Text,
                Common.MyInt.Parse(PointsEntry.Text),
                viewModel.Lefts.ToList(),
                viewModel.Rights.ToList(),
                viewModel.Relation.ToList()
            );

            try
            {
                question.Validate();
            }catch(Exception exc)
            {
                await DisplayAlert("Question is invalid", exc.Message, "Ok");
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
    }
}
