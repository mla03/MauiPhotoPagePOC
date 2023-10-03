using POCPictureGallery.Services.DatabaseServices;

namespace POCPictureGallery.Services.DatabaseService
{
    public static class DatabaseServiceConfiguration
    {
        public static MauiAppBuilder ConfigureDatabaseServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<DatabaseInstanceService>();
            builder.Services.AddSingleton<Upgrade>();
            builder.Services.AddSingleton<JobDatabaseService>();
            builder.Services.AddSingleton<PhotoDatabaseService>();
            return builder;
        }
    }
}
