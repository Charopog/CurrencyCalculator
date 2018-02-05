
using Android.Content;
using Android.Runtime;
using CurrencyCalculator.Xam.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(NoPaddingButtonRenderer))]
namespace CurrencyCalculator.Xam.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class NoPaddingButtonRenderer : ButtonRenderer
    {
        private Context _context;

        public NoPaddingButtonRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if( Control != null)
            {
                Control?.SetPadding(0, 0, 0, 0);
            }
            
        }
    }
}