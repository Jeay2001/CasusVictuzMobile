using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;

using CasusVictuzMobile.MVVM.View;

namespace CasusVictuzMobile
{
    public partial class App : Application
    {
        public static BaseRepository<Event>? EventRepository { private set; get; }
        public static BaseRepository<User>? UserRepository { private set; get; }
        public static BaseRepository<MVVM.Models.Location>? LocationRepository { private set; get; }
        public static BaseRepository<Category>? CategoryRepository { private set; get; }
        public static BaseRepository<Notification>? NotificationRepository { private set; get; }
        public static BaseRepository<Registration>? RegistrationRepository { private set; get; }
        public App(BaseRepository<Event> eventRepo, BaseRepository<User> userRepo, BaseRepository<MVVM.Models.Location> locationRepo, BaseRepository<Category> categoryRepo, BaseRepository<Notification> notificationRepo, BaseRepository<Registration> registrationRepo)
        {
            InitializeComponent();
            EventRepository = eventRepo;
            UserRepository = userRepo;
            LocationRepository = locationRepo;
            CategoryRepository = categoryRepo;
            NotificationRepository = notificationRepo;
            RegistrationRepository = registrationRepo;


            MainPage = new LoginPage();
        }
    }
}
