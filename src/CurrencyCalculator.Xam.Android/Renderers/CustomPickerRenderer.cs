using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CurrencyCalculator.Xam.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CurrencyCalculator.Xam.Droid.Renderers.CustomPickerRenderer))]
namespace CurrencyCalculator.Xam.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class CustomPickerRenderer : Xamarin.Forms.Platform.Android.PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.Gravity = GravityFlags.Center;
            }
        }
    }
}