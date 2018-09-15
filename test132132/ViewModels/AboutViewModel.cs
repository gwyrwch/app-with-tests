using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace test132132
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://yandex.ru/collections")));
        }

        public ICommand OpenWebCommand { get; }
    }
}
