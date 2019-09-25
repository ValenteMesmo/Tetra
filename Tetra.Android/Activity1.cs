using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Tetra.Android
{
    [Activity(Label = "Tetra.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private Game1 game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            game = new Game1();
            SetViewFullScreen();
            game.Run();
        }
        
        private void SetViewFullScreen()
        {
            var view = (View)game.Services.GetService(typeof(View));

            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky
                );

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;

            SetContentView(view);
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetViewFullScreen();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            SetViewFullScreen();
        }
    }
}

