

using SQLite;

namespace POCPictureGallery.Services.DatabaseServices
{
    public class DatabaseInstanceService : BaseService<DatabaseInstanceService>
    {
        private SQLiteConnection _database;

        public DatabaseInstanceService()
        {
            this.SetupDatabase();
        }
        public void SetupDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppDB.db");
            _database = new SQLiteConnection(dbPath);
        }
        public static SQLiteConnection GetDatabaseInstance()
        {
            return GetInstance()._database;
        }
    }
}
