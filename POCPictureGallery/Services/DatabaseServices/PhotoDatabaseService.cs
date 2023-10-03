using POCPictureGallery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Services.DatabaseService
{
    public class PhotoDatabaseService : BaseDatabaseService<PhotoDatabaseService, Photo>
    {
        public PhotoDatabaseService()
        {
        }

        public List<Photo> GetPhotosByJobGuid(string pJobGuid)
        {
            return _connection.Table<Photo>().Where(x => x.JobGuid == pJobGuid && x.Deleted == false).ToList();
        }

        public void SetDeletedOnPhotos(string guidToDelete) 
        {
            var sqlStatement = string.Format(@"UPDATE Photo SET Deleted = 1 WHERE PhotoGuid = '{0}' OR JobGuid = '{0}'", guidToDelete);

            _connection.Query<Photo>(sqlStatement);
        }
    }
}
