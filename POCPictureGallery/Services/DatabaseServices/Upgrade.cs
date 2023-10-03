using POCPictureGallery.Services.DatabaseServices;
using SQLite;

namespace POCPictureGallery.Services.DatabaseService
{
    public class Upgrade : BaseService<Upgrade>
    {
#if ANDROID        
        public static string ImageFolder = Path.Combine(FileSystem.Current.AppDataDirectory, "AppImages");
        public static string ExternalImageFolder = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath, "AppImages");

#elif IOS
        public static string ImageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#endif

        private readonly SQLiteConnection _database;
        public Upgrade()
        {
            if (!Directory.Exists(ImageFolder))
            {
                Directory.CreateDirectory(ImageFolder);
            }
            this._database = DatabaseInstanceService.GetDatabaseInstance();
            this.Init();
        }
        public void Init()
        {
            if (this.ShouldCreateDatabase())
            {
                this.CreateJobTable();
                this.CreatePhotoTable();
            }
        }
        public bool ShouldCreateDatabase()
        {
            var result = _database.Query<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Job'");
            return result.FirstOrDefault() == 0;
        }


        public void CreateJobTable()
        {
            try
            {
                _database.Execute(@"CREATE TABLE IF NOT EXISTS Job(
                                    Guid TEXT PRIMARY KEY,
                                    Name TEXT)");
            }
            catch (Exception ex)
            {
                throw new Exception("Error during upgrade of Job Table", ex);
            }
        }

        public void CreatePhotoTable()
        {
            try
            {
                _database.Execute(@"CREATE TABLE IF NOT EXISTS Photo(
                                    PhotoGuid TEXT PRIMARY KEY,
                                    Deleted BIT ,
                                    FileLocation TEXT,
                                    JobGuid TEXT)");
            }
            catch (Exception ex)
            {
                new Exception("Error during upgrade of Photo Table", ex);
            }
        }
    }
}
