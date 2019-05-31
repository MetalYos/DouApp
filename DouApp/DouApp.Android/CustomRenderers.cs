using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using DouApp.CustomControls;
using DouApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace DouApp.Droid
{
    public class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            MyEntry entry = Element as MyEntry;
            if (entry.BorderWidth > 0)
                DrawBorder(Control, entry);
        }

        private void DrawBorder(Android.Widget.EditText control, MyEntry entry)
        {
            var nativeEditText = (Android.Widget.EditText)Control;

            ShapeDrawable shape = null;
            if (entry.BorderRadius > 0)
            {
                float[] outerRadii = new float[] 
                {
                    entry.BorderRadius, entry.BorderRadius,
                    entry.BorderRadius, entry.BorderRadius,
                    entry.BorderRadius, entry.BorderRadius,
                    entry.BorderRadius, entry.BorderRadius
                };

                shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RoundRectShape(outerRadii, null, null));
            }
            else
            {
                shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
            }
            shape.Paint.Color = entry.BorderColor.ToAndroid();
            shape.Paint.SetStyle(Paint.Style.Stroke);
            shape.Paint.StrokeWidth = entry.BorderWidth;
            nativeEditText.Background = shape;
        }
    }
}