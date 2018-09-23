using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using test132132.iOS;
using System.ComponentModel;
using CoreAnimation;
using Foundation;

[assembly: ExportRenderer(typeof(test132132.Common.LineEntry), typeof(LineEntryRenderer))]
namespace test132132.iOS
{
    public class LineEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

                var view = (Element as Common.LineEntry);
                if (view != null)
                {
                    DrawBorder(view);
                    SetFontSize(view);
                    SetPlaceholderTextColor(view);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (Common.LineEntry)Element;

            if (e.PropertyName.Equals(view.BorderColor))
                DrawBorder(view);
            if (e.PropertyName.Equals(view.FontSize))
                SetFontSize(view);
            if (e.PropertyName.Equals(view.PlaceholderColor))
                SetPlaceholderTextColor(view);
        }

        void DrawBorder(Common.LineEntry view)
        {
            var borderLayer = new CALayer();
            borderLayer.MasksToBounds = true;
            borderLayer.Frame = new CoreGraphics.CGRect(0f, Frame.Height / 2, Frame.Width, 1f);
            borderLayer.BorderColor = view.BorderColor.ToCGColor();
            borderLayer.BorderWidth = 0.5f;

            Control.Layer.AddSublayer(borderLayer);
            Control.BorderStyle = UITextBorderStyle.None;
        }

        void SetFontSize(Common.LineEntry view)
        {
            double EPS = 1e-9;
            if (System.Math.Abs(view.FontSize - Font.Default.FontSize) > EPS)
                Control.Font = UIFont.SystemFontOfSize((System.nfloat)view.FontSize);
            else if (System.Math.Abs(view.FontSize - Font.Default.FontSize) < EPS)
                Control.Font = UIFont.SystemFontOfSize(17f);
        }

        void SetPlaceholderTextColor(Common.LineEntry view)
        {
            if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderColor != Color.Default)
            {
                var placeholderString = new NSAttributedString(view.Placeholder,
                                            new UIStringAttributes { ForegroundColor = view.PlaceholderColor.ToUIColor() });
                Control.AttributedPlaceholder = placeholderString;
            }
        }
    }
}
