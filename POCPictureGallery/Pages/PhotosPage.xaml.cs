
using POCPictureGallery.Pages;
using POCPictureGallery.Resources;
using POCPictureGallery.Services;
using POCPictureGallery.ViewModels;

namespace POCPictureGallery;

public partial class PhotosPage : ContentPage, CanNavigateFrom
{
	private PhotosViewModel vm => BindingContext as PhotosViewModel;

	public PhotosPage(PhotosViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        vm.InitializeViewModel();
        CollectionViewPhotoList.SelectedItem = null;

    }
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        vm.SavePhotos();
    }

    protected override bool OnBackButtonPressed()
	{
		return true;
	}

	public async void OnBackButtonClicked(object sender, EventArgs args)
	{
        await NavigationService.Navigate("..", true);
    }

    private async void FromGallery_Clicked(object sender, EventArgs e)
    {
        var result = await vm.PickAPicture();
        if (result != null)
            await DisplayAlert("Error adding new photo", result.ToString(), "OK");
    }

	private async void TakePicture_Clicked(object sender, EventArgs e)
    {
        var result = await vm.TakePhotoAsync();
        if (result != null)
			await DisplayAlert("Error adding new photo", result.ToString(), "OK");
    }

    
    public async Task<bool> CanNavigateFrom()
    {
        var response = true;
        if (String.IsNullOrEmpty(vm.JobGuid))
        {
            response = await DisplayAlert("Unsaved Changes", "Are you sure you want to cancel your changes?", "Yes", "No");
        }
        return response;
    }
}