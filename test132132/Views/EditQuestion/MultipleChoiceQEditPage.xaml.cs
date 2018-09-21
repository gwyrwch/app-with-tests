using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class MultipleChoiceQEditPage : ContentPage
    {
        ViewModels.Editor.MultipleChoiceQViewModel viewModel;

        public MultipleChoiceQEditPage()
        {
            InitializeComponent();
            AmountOfVariansPicker.ItemsSource = Enumerable.Range(1, 10).ToList();
            BindingContext = viewModel = new ViewModels.Editor.MultipleChoiceQViewModel();

        }

        void AmountSelected (object sender, EventArgs e) {
            int length = (int)AmountOfVariansPicker.SelectedItem;
            viewModel.SetListLength(length);
        }

        public async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.SaveAll();
            Models.MultipleChoiceQuestion question = new Models.MultipleChoiceQuestion(
                QuestionText.Text, 
                int.Parse(Points.Text),
                viewModel.Variants.ToList()
            );
            MessagingCenter.Send(this, "CreateNewQuestion", question);
            await Navigation.PopToRootAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage()); //todo
        }
    }
}
