using POCPictureGallery.Pages;
using POCPictureGallery.Services.DatabaseService;

namespace POCPictureGallery
{
    public partial class App : Application
    {
        public App()
        {
            Current.UserAppTheme = AppTheme.Light;
            InitializeComponent();
            Upgrade.GetInstance();
            SetupRoutes();

            MainPage = new AppShell();
        }

        public void SetupRoutes()
        {
            Routing.RegisterRoute(nameof(JobPage), typeof(JobPage));
            Routing.RegisterRoute(nameof(PhotosPage), typeof(PhotosPage));
        }
    }
}
