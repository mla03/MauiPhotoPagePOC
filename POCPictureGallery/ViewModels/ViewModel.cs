using CommunityToolkit.Mvvm.ComponentModel;
using POCPictureGallery.Services;

namespace POCPictureGallery.ViewModels
{
    public partial class ViewModel<T> : ObservableObject
    {
        public static T GetInstance()
        {
            return ServicesProvider.GetService<T>();
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;
    }
}
