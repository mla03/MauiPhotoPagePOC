using POCPictureGallery.Services;
using POCPictureGallery.ViewModel;

namespace POCPictureGallery.Pages;

public partial class JobPage : ContentPage
{
    private JobViewModel vm => BindingContext as JobViewModel;

    public JobPage(JobViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        vm.ReloadJob();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await NavigationService.Navigate($"{nameof(PhotosPage)}?JobGuid={vm.Job.Guid}");
    }
}