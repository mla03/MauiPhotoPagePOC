using CommunityToolkit.Mvvm.ComponentModel;
using POCPictureGallery.Model;
using POCPictureGallery.Services;
using POCPictureGallery.Services.DatabaseService;
using POCPictureGallery.ViewModels;

namespace POCPictureGallery.ViewModel
{
    public partial class JobViewModel : ViewModel<JobViewModel>
    {

        private readonly JobService  _jobService;
        private readonly JobDatabaseService _jobDatabaseService;

        [ObservableProperty]
        private Job job;

        private string JobGuid;


        public JobViewModel()
        {
            _jobService = JobService.GetInstance();
            _jobDatabaseService = JobDatabaseService.GetInstance();
            CreateJobIfNotAlreadyThere();
        }

        public void CreateJobIfNotAlreadyThere()
        {
            Job = _jobDatabaseService.GetAll().FirstOrDefault();
            if (Job == null)
            {
                JobGuid = Guid.NewGuid().ToString();
                Job = new Job() { Guid = JobGuid };
                _jobService.SetJob(Job, true);
                _jobService.Initialize(Job.Guid);
                Job.MarkPrestine();
            }
            else
            {
                JobGuid = Job.Guid;
                _jobService.Initialize(JobGuid);
                Job = _jobService.GetJob();
            }
        }

        public void ReloadJob()
        {
            if(Job?.Guid != null)
            {
                Job = _jobService.GetJob();
            }
        }

    }
}
