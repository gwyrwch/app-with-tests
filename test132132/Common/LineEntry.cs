using Xamarin.Forms;

namespace test132132.Common
{
    public class LineEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty =
#pragma warning disable CS0618 // Тип или член устарел
#pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
            BindableProperty.Create<LineEntry, Color>(
                p => p.BorderColor, 
                Color.Silver
            );

        public Color BorderColor {
            get { return (Color)GetValue (BorderColorProperty); }
            set { SetValue (BorderColorProperty, value); }
        }
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create<LineEntry, double> (p => p.FontSize, Font.Default.FontSize);

        public double FontSize {
            get { return (double)GetValue (FontSizeProperty); }
            set { SetValue (FontSizeProperty, value); }
        }

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create<LineEntry, Color> (p => p.PlaceholderColor, Color.Default);

        public Color PlaceholderColor {
            get { return (Color)GetValue (PlaceholderColorProperty); }
            set { SetValue (PlaceholderColorProperty, value); }
        }

#pragma warning restore CS0618 // Тип или член устарел
#pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
    }
}
