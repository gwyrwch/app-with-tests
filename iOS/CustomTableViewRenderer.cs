using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using test132132.Common;
using Xamarin.Forms.Platform.iOS;
using Foundation;

[assembly: ExportRenderer(typeof(CustomTableView), typeof(test132132.iOS.CustomTableViewRenderer))]
namespace test132132.iOS
{
    public class CustomTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;
            if (Element == null)
                return;
 
            var tableView = Control as UITableView;
            var CustomTableView = Element as CustomTableView;
            tableView.WeakDelegate = new CustomHeaderTableModelRenderer(CustomTableView);
        }
 
        private class CustomHeaderTableModelRenderer : UnEvenTableViewModelRenderer
        {
            private readonly CustomTableView _CustomTableView;
            public CustomHeaderTableModelRenderer(TableView model) : base(model)
            {
                _CustomTableView = model as CustomTableView;
            }

            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return 40;
            }

            public override UIView GetViewForHeader(UITableView tableView, nint section)
            {
                var a = new UILabel()
                {
                    Text = "  " + TitleForHeader(tableView, section),
                    TextColor = _CustomTableView.GroupHeaderColor.ToUIColor(),
                    //TextAlignment = UITextAlignment.Justified,
                    // BackgroundColor = Color.Orange.ToUIColor(),
                    BackgroundColor = ((Color)Xamarin.Forms.Application.Current.Resources["TableSectionColor"]).ToUIColor(),
                    //Margin
                    TextAlignment = UITextAlignment.Natural,
                    Font = UIFont.BoldSystemFontOfSize(16)
                };

                var width = a.Frame.Width;
                var frame = a.Frame;

                frame.Size = new CoreGraphics.CGSize(width, width);
                a.Frame = frame;    

                return a;
            }
        }   
    }
}
