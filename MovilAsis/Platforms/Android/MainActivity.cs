using Android.App;
using Android.Content.PM;


namespace MovilAsis
{
    [Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);
        //    HandleIntent(Intent);
        //    CreateNotificationChannelIfNeeded();
        //}

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //    HandleIntent(intent);
        //}

        //private static void HandleIntent(Intent intent)
        //{
        //    FirebaseCloudMessagingImplementation.OnNewIntent(intent);
        //}

        //private void CreateNotificationChannelIfNeeded()
        //{
        //    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        //    {
        //        CreateNotificationChannel();
        //    }
        //}

        //private void CreateNotificationChannel()
        //{
        //    var channelId = $"{PackageName}.general";
        //    var notificationManager = (NotificationManager)GetSystemService(NotificationService);
        //    var channel = new NotificationChannel(channelId, "General", NotificationImportance.Default);
        //    notificationManager.CreateNotificationChannel(channel);
        //    FirebaseCloudMessagingImplementation.ChannelId = channelId;
        //    FirebaseCloudMessagingImplementation.SmallIconRef = Resource.Drawable.abc_ic_search_api_material;
        //}




        //private ActivityResultLauncher notificationPermissionLauncher;
        //private const int RequestPostNotificationId = 1001;

        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    // Inicializar Firebase
        //    InitializeFirebase();

        //    // Configurar el launcher para permisos de notificación
        //    SetupNotificationPermissionLauncher();

        //    // Solicitar permisos de notificación para Android 13+
        //    RequestNotificationPermissionIfNeeded();

        //    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        //    {
        //        Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#2F3E46"));
        //    }
        //}

        //private void InitializeFirebase()
        //{
        //    try
        //    {
        //        Firebase.FirebaseApp.InitializeApp(this);
        //        CrossFirebaseCloudMessaging.Current.SubscribeToTopicAsync("accident_alerts");
        //        HandleNotificationIntent(Intent);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error inicializando Firebase: {ex.Message}");
        //    }
        //}

        //private void SetupNotificationPermissionLauncher()
        //{
        //    var callback = new ActivityResultCallback();
        //    callback.ResultReceived += (isGranted) =>
        //    {
        //        if (!isGranted)
        //        {
        //            System.Diagnostics.Debug.WriteLine("Permiso de notificaciones denegado");
        //        }
        //    };

        //    notificationPermissionLauncher = RegisterForActivityResult(
        //        new ActivityResultContracts.RequestPermission(),
        //        callback);
        //}

        //private void RequestNotificationPermissionIfNeeded()
        //{
        //    if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
        //    {
        //        if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.PostNotifications) != Permission.Granted)
        //        {
        //            notificationPermissionLauncher.Launch(Manifest.Permission.PostNotifications);
        //        }
        //    }
        //}

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //    HandleNotificationIntent(intent);
        //}

        //private void HandleNotificationIntent(Intent intent)
        //{
        //    if (intent?.Extras != null)
        //    {
        //        var notification = new RemoteMessage(intent.Extras).GetNotification();
        //        if (notification != null)
        //        {
        //            System.Diagnostics.Debug.WriteLine($"Notificación recibida: {notification.Title} - {notification.Body}");
        //        }
        //    }
        //}
        //private class ActivityResultCallback : Java.Lang.Object, IActivityResultCallback
        //{
        //    public event Action<bool> ResultReceived;

        //    public void OnActivityResult(Java.Lang.Object isGranted)
        //    {
        //        ResultReceived?.Invoke((bool)isGranted);
        //    }
        //}
    }

}