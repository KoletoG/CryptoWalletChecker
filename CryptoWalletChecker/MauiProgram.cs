using Microsoft.Extensions.Logging;
using Serilog;

namespace CryptoWalletChecker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Log.Logger = new LoggerConfiguration()
           .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Audiowide-Regular.ttf", "AudiowideRegular");
                    fonts.AddFont("Tomorrow-Black.ttf", "TomorrowBlack");
                    fonts.AddFont("Tomorrow-Regular.ttf", "TomorrowRegular");
                })
                .Services.AddLogging(logging=>
                {
                    logging.AddSerilog();
                })
                         .AddSingleton<IMethodsServices,MethodsService>()
                         .AddSingleton<MainPage>()
                ;
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
