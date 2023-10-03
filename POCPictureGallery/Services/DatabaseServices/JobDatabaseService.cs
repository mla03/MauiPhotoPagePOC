using POCPictureGallery.Model;

namespace POCPictureGallery.Services.DatabaseService
{
    public class JobDatabaseService : BaseDatabaseService<JobDatabaseService, Job>
    {
        private readonly PhotoDatabaseService _photoDatabaseService;
        public JobDatabaseService()
        {
            _photoDatabaseService = PhotoDatabaseService.GetInstance();
        }

        public Job GetJobWithSubObjectByGuid(string pGuid)
        {
            Job job = base.GetByGuid<Job>(pGuid);

            if(job != null)
            {
                job.Documents = _photoDatabaseService.GetPhotosByJobGuid(pGuid);
            }
            
            return job;
        }
    }
}
