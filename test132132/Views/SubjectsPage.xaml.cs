﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace test132132
{
    public partial class SubjectsPage : ContentPage
    {
        ViewModels.Tests.SubjectsViewModel viewModel;
        public SubjectsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ViewModels.Tests.SubjectsViewModel();



            //CSharpTableSection.BindingContext = viewModel.subjects; //todo ???

            //SubjectsListView.ItemsSource = App.Subjects;
            //SubjectsTableView.Resources = App.Subjects;
        }

        void RenderTableView() {
            // viewModel.SplitBySubject сейчас возвращает все что нужно отрендерить
            // создать TableSection-ов и в каждый перебить в ListView его группу
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // todo
            //var item = args.SelectedItem as string;
            //if (item == null)
              //  return;

            await Navigation.PushAsync(new AboutPage());

           // SubjectsListView.SelectedItem = null;
        }
    }

}
