using POCPictureGallery.Resources;

namespace POCPictureGallery.Services
{
    public class PermissionService : BaseService<PermissionService>
    {
        public async Task<PermissionStatus> CheckAndRequestCameraPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                await Application.Current.MainPage.DisplayAlert("App", AppResources.Permission_Camera_iOS, "Cancel");
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.Camera>())
            {
                // Prompt the user with additional information as to why the permission is needed
                await Application.Current.MainPage.DisplayAlert("App", AppResources.Permission_Camera_Android, "Cancel");
            }

            status = await Permissions.RequestAsync<Permissions.Camera>();

            return status;
        }

        public async Task<PermissionStatus> CheckAndRequestPhotosAddOnly()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.PhotosAddOnly>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                await Application.Current.MainPage.DisplayAlert("App", "You need to Grant permission to AddPhotos in Settings.", "Cancel");
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.PhotosAddOnly>();

            return status;
        }
    }
}
