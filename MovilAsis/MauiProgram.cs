using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MovilAsis.Services;


namespace MovilAsis
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            try
            {
                var builder = MauiApp.CreateBuilder();

                builder
                    .UseMauiApp<App>()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    });

                var app = builder.Build();


                return app;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public static MauiAppBuilder RegistrarServicioFirebase(this MauiAppBuilder builder)
//        {
//            Debug.WriteLine("Iniciando RegistrarServicioFirebase...");

//            try
//            {
//                builder.ConfigureLifecycleEvents(evento =>
//                {
//                    Debug.WriteLine("Configurando eventos de ciclo de vida...");

//#if ANDROID
//                    Debug.WriteLine("Configurando para Android...");
//                    evento.AddAndroid(Android => Android.OnCreate((activity, bundle) =>
//                    {
//                        Debug.WriteLine("Evento OnCreate de Android activado");
                        
//                        try
//                        {
//                            Debug.WriteLine("Intentando inicializar FirebaseApp...");
//                            var app = Firebase.FirebaseApp.InitializeApp(activity);
                            
//                            if (app == null)
//                            {
//                                Debug.WriteLine("Primer intento de inicialización falló, intentando nuevamente...");
//                                Firebase.FirebaseApp.InitializeApp(activity);
//                                Debug.WriteLine("Segundo intento de inicialización completado");
//                            }
//                            else
//                            {
//                                Debug.WriteLine("FirebaseApp inicializado correctamente en primer intento");
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            Debug.WriteLine($"Error al inicializar Firebase en OnCreate: {ex.ToString()}");
//                            throw;
//                        }
//                    }));
//                    Debug.WriteLine("Configuración de Android completada");
//#endif
//                });

//                Debug.WriteLine("Registrando CrossFirebaseAuth...");
//                builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
//                Debug.WriteLine("CrossFirebaseAuth registrado");
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine($"Error en RegistrarServicioFirebase: {ex.ToString()}");
//                throw;
//            }

//            return builder;
//        }
        
        
//        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
//        {
//            builder.ConfigureLifecycleEvents(events => {
//#if IOS
//            events.AddiOS(iOS => iOS.WillFinishLaunching((app, launchOptions) => {
//                CrossFirebase.Initialize(CreateCrossFirebaseSettings());
//                FirebaseAuthFacebookImplementation.Initialize(app, launchOptions, "151743924915235", "Plugin Firebase Playground");
//                FirebaseAuthGoogleImplementation.Initialize();
//                return false;
//            }));
//#elif ANDROID
//                events.AddAndroid(android => android.OnCreate((activity, _) => {
//                    var settings = CreateCrossFirebaseSettings();
//                    CrossFirebase.Initialize(activity, settings);
//                    //FirebaseAuthGoogleImplementation.Initialize(settings.GoogleRequestIdToken);
//                }));
//#endif
//            });

//            builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
//            //builder.Services.AddSingleton(_ => CrossFirebaseAuthFacebook.Current);
//            //builder.Services.AddSingleton(_ => CrossFirebaseAuthGoogle.Current);
//            builder.Services.AddSingleton(_ => CrossFirebaseCloudMessaging.Current);
//            builder.Services.AddSingleton(_ => CrossFirebaseDynamicLinks.Current);
//            builder.Services.AddSingleton(_ => CrossFirebaseFunctions.Current);
//            builder.Services.AddSingleton(_ => CrossFirebaseStorage.Current);
//            builder.Services.AddSingleton(_ => CrossFirebaseRemoteConfig.Current);
//            return builder;
//        }


        //public static CrossFirebaseSettings CreateCrossFirebaseSettings()
        //{
        //    Debug.WriteLine("Creando CrossFirebaseSettings...");
        //    return new CrossFirebaseSettings(
        //            isAnalyticsEnabled: true,
        //            isAuthEnabled: true,
        //            isCloudMessagingEnabled: true,
        //            isDynamicLinksEnabled: true,
        //            isFirestoreEnabled: true,
        //            isFunctionsEnabled: true,
        //            isRemoteConfigEnabled: true,
        //            isStorageEnabled: true,
        //            googleRequestIdToken: "537235599720-723cgj10dtm47b4ilvuodtp206g0q0fg.apps.googleusercontent.com");
        //}
    }
}