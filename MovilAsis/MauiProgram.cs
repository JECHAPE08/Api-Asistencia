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
    }
}