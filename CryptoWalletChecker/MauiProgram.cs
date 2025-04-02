using Microsoft.Extensions.Logging;

namespace CryptoWalletChecker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
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
                .Services.AddSingleton<IMethodsServices,MethodsService>()
                         .AddSingleton<MainPage>()
                ;
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
