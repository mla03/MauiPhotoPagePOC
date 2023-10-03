using CommunityToolkit.Mvvm.ComponentModel;
using POCPictureGallery.Resources;
using POCPictureGallery.Model;
using POCPictureGallery.Services;
using POCPictureGallery.Services.DatabaseService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.ViewModels
{
    [QueryProperty(nameof(JobGuid), nameof(JobGuid))]
    public partial class PhotosViewModel : ViewModel<PhotosViewModel>
    {
        private readonly JobService _jobService;
        private readonly PermissionService _permissionService;

        public PhotosViewModel()
        {
            _jobService = JobService.GetInstance();
            _permissionService = PermissionService.GetInstance();
        }

        [ObservableProperty]
        private string jobGuid;

        [ObservableProperty]
        private ObservableCollection<Photo> photoList = new ObservableCollection<Photo>();

        [ObservableProperty]
        private Job job;

        public void InitializeViewModel()
        {
            _jobService.Initialize(JobGuid);
            Job = _jobService.GetJob();

            UpdatePhotoLists();
            OnPropertyChanged(nameof(PhotoList));
        }

        public void UpdatePhotoLists()
        {
            PhotoList?.Clear();

            if (Job.Documents != null)
            {
                foreach (var photo in Job.Documents)
                {
                    PhotoList.Add(photo);
                }
                OnPropertyChanged(nameof(PhotoList));
            }
        }

        public void SavePhotos()
        {
            _jobService.SetJob(Job, false, true);
        }

        public async Task<string> PickAPicture()
        {
            string returnMsg = null;

            try
            {
                var fileResults = new List<FileResult>();
                fileResults.Add(await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "App" }));
                if (fileResults != null)
                {
                    foreach (FileResult image in fileResults)
                    {
                        var newFile = Path.Combine(Upgrade.ImageFolder, image.FileName);
                        using (var stream = await image.OpenReadAsync())
                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        var photo = Photo.InitializeNewPhoto(image.FileName, Job.Guid);
                        Job.Documents.Add(photo);
                        PhotoList.Add(photo);
                        Job.MarkDirty();
                    }
                    OnPropertyChanged(nameof(PhotoList));
                }
            }
            catch (NullReferenceException)
            {
                returnMsg = null; //user most likely pressed cancel, we need no response.
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            return returnMsg;
        }

        public async Task<string> TakePhotoAsync()
        {
            var status = await _permissionService.CheckAndRequestCameraPermission();
            if (status == PermissionStatus.Granted)
            {
                string returnMsg = null;

                try
                {
                    var photo = await MediaPicker.CapturePhotoAsync();

                    if (photo != null)
                    {
                        // save the file into local storage
                        var newFile = Path.Combine(Upgrade.ImageFolder, photo.FileName);
                        using (var stream = await photo.OpenReadAsync())
                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        var newPhoto = Photo.InitializeNewPhoto(photo.FileName, Job.Guid);
                        Job.Documents.Add(newPhoto);
                        PhotoList.Add(newPhoto);
                        Job.MarkDirty();

                        OnPropertyChanged(nameof(PhotoList));

                        Console.WriteLine($"CapturePhotoAsync COMPLETED: {newPhoto}");
                    }
                    else
                    {
                        returnMsg = "No Photo received from camera";
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    Console.WriteLine($"CapturePhotoAsync THREW: {fnsEx.Message}");
                    returnMsg = fnsEx.Message; // Feature is not supported on the device
                }
                catch (PermissionException pEx)
                {
                    Console.WriteLine($"CapturePhotoAsync THREW: {pEx.Message}");
                    returnMsg = pEx.Message + "\n" + AppResources.Permission_TakePicture;// Permissions not granted
                }
                catch (NullReferenceException)
                {
                    returnMsg = null; //user most likely pressed cancel, we need no response.
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                    returnMsg = ex.Message;
                }

                return returnMsg;
            }
            else
            {
                return "";
            }

        }
    }
}
