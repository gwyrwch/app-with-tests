using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views
{
    public partial class EditorTestsPage : ContentPage
    {
        ViewModels.Editor.EditorTestsViewModel viewModel;

        public EditorTestsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.Editor.EditorTestsViewModel();

            MessagingCenter.Subscribe<EditorPage, Models.Test>(
                this, "CreateNewTest", (obj, test) =>
                {
                    viewModel.Add(test);
                }
            );
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var selectedItem = UserTestsListView.SelectedItem; // todo
            UserTestsListView.SelectedItem = null;
        }

        public async void Add_Clicked(object sender, EventArgs e) 
        {
            await Navigation.PushAsync(new EditorPage());
        }

    }
}
