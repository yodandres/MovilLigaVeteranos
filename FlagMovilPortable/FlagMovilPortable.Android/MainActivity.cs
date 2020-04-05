
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using Plugin.Permissions;

namespace FlagMovilPortable.Droid
{
    [Activity(Label = "Liga Veteranos", Icon = "@drawable/liga_veteranos_1", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        ////#region Singleton
        ////private static MainActivity instance;

        ////public static MainActivity GetInstance()
        ////{
        ////    if (instance == null)
        ////    {
        ////        instance = new MainActivity();
        ////    }

        ////    return instance;
        ////}
        ////#endregion

        #region Methods
        protected override void OnCreate(Bundle bundle)
        {
            ////instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        #endregion

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

