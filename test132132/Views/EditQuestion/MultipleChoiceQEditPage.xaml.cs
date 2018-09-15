using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace test132132
{
    public partial class MultipleChoiceQEditPage : ContentPage
    {
        //ObservableCollection<Models.Variant> variants;
        ViewModels.Editor.MultipleChoiceQViewModel viewModel;

        public MultipleChoiceQEditPage()
        {
            InitializeComponent();
            AmountOfVariansPicker.ItemsSource = Enumerable.Range(1, 10).ToList();
            BindingContext = viewModel = new ViewModels.Editor.MultipleChoiceQViewModel();
            //BindingMode = BindingMode.TwoWay;

        }

        void AmountSelected (object sender, EventArgs e) {
            int length = (int)AmountOfVariansPicker.SelectedItem;
            viewModel.SetListLength(length);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage());
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage());
        }
    }
}
