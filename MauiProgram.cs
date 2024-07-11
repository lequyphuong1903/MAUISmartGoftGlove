using Syncfusion.Maui.Core.Hosting;
using Microsoft.Extensions.Logging;
using Firebase.Database;
using SmartGolfGlove_V2.Views;

namespace SmartGolfGlove_V2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(new FirebaseClient("https://wristmotionofglove-default-rtdb.firebaseio.com/"));
            builder.Services.AddSingleton<Personal>();
            builder.Services.AddSingleton<AboutUs>();
            return builder.Build();
        }
    }
}
