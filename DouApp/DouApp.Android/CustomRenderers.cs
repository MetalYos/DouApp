using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using DouApp.CustomControls;
using DouApp.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
[assembly: ExportRenderer(typeof(MyPicker), typeof(MyPickerRenderer))]
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

            if (Control == null || e.NewElement == null)
                return;

            MyEntry entry = Element as MyEntry;
            /*
            if (entry.BorderWidth > 0)
                DrawBorder(Control, entry);
            */
            DrawLine(Control, entry.LineColor.ToAndroid());
            SetDisabledColors(entry.DisabledColor.ToAndroid());
        }

        private void DrawLine(Android.Widget.EditText control, Android.Graphics.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            else
                control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
        }

        private void SetDisabledColors(Android.Graphics.Color color)
        {
            Control.SetTextColor(Element.IsEnabled ? Element.TextColor.ToAndroid() : color);
            //Control.SetBackgroundColor(Element.IsEnabled ? Element.BackgroundColor.ToAndroid() : Android.Graphics.Color.DarkGray);
        }

        /*
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
        */
    }

    public class MyPickerRenderer : PickerRenderer
    {
        public MyPickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control == null && Element == null)
                return;

            MyPicker entry = Element as MyPicker;
            DrawLine(Control, entry.LineColor.ToAndroid());
        }

        private void DrawLine(Android.Widget.EditText control, Android.Graphics.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            else
                control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
        }
    }
}