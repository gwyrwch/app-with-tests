using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace test132132
{
    public partial class SubjectsPage : ContentPage
    {
        public SubjectsPage()
        {
            InitializeComponent();
            SubjectsListView.ItemsSource = App.Subjects;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as string;
            if (item == null)
                return;

            await Navigation.PushAsync(new AboutPage());

            SubjectsListView.SelectedItem = null;
        }
    }

}
