using POCPictureGallery.Services;
using POCPictureGallery.Services.DatabaseService;

namespace POCPictureGallery.Services
{
    public static class ServiceConfiguration
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {

            builder.Services.AddSingleton<JobService>();
            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddSingleton<PermissionService>();
            builder
                .ConfigureDatabaseServices();
            return builder;
        }
    }
}
