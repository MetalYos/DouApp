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
            SetDisabledColors(Control, entry.DisabledColor.ToAndroid());
        }

        private void DrawLine(Android.Widget.EditText control, Android.Graphics.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                control.BackgroundTintList = ColorStateList.ValueOf(color);
            else
                control.Background.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
        }

        private void SetDisabledColors(Android.Widget.EditText control, Android.Graphics.Color color)
        {
            control.SetTextColor(Element.IsEnabled ? Element.TextColor.ToAndroid() : color);
        }
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
                control.BackgroundTintList = ColorStateList.ValueOf(color);
            else
                control.Background.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
        }
    }
}