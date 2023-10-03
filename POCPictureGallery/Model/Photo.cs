using POCPictureGallery.Services.DatabaseService;
using SQLite;

namespace POCPictureGallery.Model
{
    public class Photo : BaseModel<Photo>
    {
        [PrimaryKey]
        public string PhotoGuid { get; set; }
        public bool Deleted { get; set; } = false;
        public string FileLocation { get; set; }
        public string JobGuid { get; set; } 
        public string GetFileLocation
        {
            get
            {
                return Path.Combine(Upgrade.ImageFolder, FileLocation);
            }
        }

        public static Photo InitializeNewPhoto(string fileLocation, string jobGuid)  
        {
            var photo = new Photo
            {
                PhotoGuid = System.Guid.NewGuid().ToString(),
                JobGuid = jobGuid,
                FileLocation = fileLocation,
            };
            return photo;
        }


    }
}
