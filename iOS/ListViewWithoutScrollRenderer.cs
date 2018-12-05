using System;
using test132132.Common;
using test132132.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListViewWithoutScroll), typeof(ListViewWithoutScrollRenderer))]
namespace test132132.iOS.Renderers
{
    public class ListViewWithoutScrollRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                Control.ScrollEnabled = false;
            }
        }
    }
}


