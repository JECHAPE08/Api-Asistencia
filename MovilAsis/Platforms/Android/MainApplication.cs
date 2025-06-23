using Android.App;
using Android.Runtime;

namespace MovilAsis
{
    [Application]
    [MetaData("com.google.firebase.messaging.default_notification_channel_id", Value = "default_channel_id")]

    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
