using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using Microsoft.Extensions.Logging;

namespace CasusVictuzMobile
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
                });
            builder.Services.AddSingleton<BaseRepository<User>>();
            builder.Services.AddSingleton<BaseRepository<Event>>();
            builder.Services.AddSingleton<BaseRepository<Registration>>();
            builder.Services.AddSingleton<BaseRepository<MVVM.Models.Location>>();            
            builder.Services.AddSingleton<BaseRepository<Notification>>();
            builder.Services.AddSingleton<BaseRepository<Category>>();
            builder.Services.AddSingleton<BaseRepository<Comment>>();
            builder.Services.AddSingleton<BaseRepository<EventRecap>>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
