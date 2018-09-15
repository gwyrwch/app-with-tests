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
            await Navigation.PushAsync(new QTypeSelectionPage());
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QTypeSelectionPage());
        }

    }
}
