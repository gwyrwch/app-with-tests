using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using test132132.Common;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ColoredTableView), typeof(test132132.iOS.ColoredTableViewRenderer))]
namespace test132132.iOS
{
    public class ColoredTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;
            if (Element == null)
                return;

            var tableView = Control as UITableView;
            var coloredTableView = Element as ColoredTableView;
            tableView.SeparatorColor = coloredTableView.SeparatorColor.ToUIColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "SeparatorColor")
            {
                var tableView = Control as UITableView;
                var coloredTableView = Element as ColoredTableView;

                tableView.SeparatorColor = coloredTableView.SeparatorColor.ToUIColor();
            }
        }
    }
}
