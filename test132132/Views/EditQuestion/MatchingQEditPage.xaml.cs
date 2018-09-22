using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class MatchingQEditPage : ContentPage
    {
        public MatchingQEditPage()
        {
            InitializeComponent();
            NumberOfAnswersPicker.ItemsSource = Enumerable.Range(1, 10).ToList();
        }

        void AmountSelected(object sender, EventArgs e)
        {
            int length = (int)NumberOfAnswersPicker.SelectedItem;
            //viewModel.SetListLength(length);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(); //todo
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage()); // todo
        }
    }
}
