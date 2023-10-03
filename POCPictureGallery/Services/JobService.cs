using POCPictureGallery.Model;
using POCPictureGallery.Services.DatabaseService;

namespace POCPictureGallery.Services
{
    internal class JobService : BaseService<JobService>
    {
        private readonly JobDatabaseService _jobDatabaseService;
        private readonly PhotoDatabaseService _photoDatabaseService;

        private string jobGuid = null;

        private Job job = null;

        public JobService()
        {
            _jobDatabaseService = JobDatabaseService.GetInstance();
            _photoDatabaseService = PhotoDatabaseService.GetInstance();
        }

        public void Initialize(string jobHistoryGuid = null)
        {
            this.jobGuid = jobHistoryGuid;
        }

        public void Clear()
        {
            this.jobGuid = null;
            this.job = null;
        }

        public Job GetJob()
        {
            if (!String.IsNullOrEmpty(jobGuid))
            {
                job = _jobDatabaseService.GetJobWithSubObjectByGuid(jobGuid);
                job?.MarkPrestine();
            }

            return job;
        }

        public void SetJob(Job jobToBeSet, bool forceSaveToDB = false, bool savePhotosToDB = false)
        {
            job = jobToBeSet;
                
            if (forceSaveToDB || (!String.IsNullOrEmpty(jobGuid) && job.IsDirty))
            {
                _jobDatabaseService.InsertOrReplace(job);

                if (job.Documents?.Count > 0 && (savePhotosToDB))
                {
                    _photoDatabaseService.InsertOrReplaceList(job.Documents);
                }
            }
            if (job != null && job.Documents?.Count > 0)
                job.Documents.RemoveAll(x => x.Deleted == true);
        }

        public void DeleteDocument(string ObjectGuid)
        {
            _photoDatabaseService.SetDeletedOnPhotos(ObjectGuid);
        }
    }
}
