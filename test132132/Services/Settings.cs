using System;
using Xamarin.Forms;

namespace test132132.Services
{
    public class Settings
    {
        public string Language { get; set; }
        public Theme Theme { get; set; }
    }

    public class Theme
    {
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
    }
}
