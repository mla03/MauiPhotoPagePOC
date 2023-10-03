using POCPictureGallery.Pages;
using POCPictureGallery.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Services
{
    public class NavigationService : BaseService<NavigationService>
    {
        private static Page _currentPage;
        public static async Task<bool> Navigate(string pUrl, bool SkipCanNavigateFrom = false, bool PopToRootBeforeNavigating = false, bool PopToRoot = false, Dictionary<string, object> navigationsParams = null)
        {
            bool canNavigateFrom = true;
            _currentPage = Shell.Current.CurrentPage;
            if (string.IsNullOrEmpty(pUrl) && !PopToRoot)
            {
                return false;
            }
            if(_currentPage != null)
            {
                if(_currentPage is CanNavigateFrom && !SkipCanNavigateFrom)
                {
                    canNavigateFrom = await (_currentPage as CanNavigateFrom).CanNavigateFrom();
                }
            }
            if (canNavigateFrom)
            {
                List<Page> stack = new List<Page>();
                if (PopToRootBeforeNavigating || PopToRoot)
                {
                    if(DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        await Shell.Current.Navigation.PopToRootAsync(false);
                    } 
                    else
                    {
                        stack.AddRange(Shell.Current.Navigation.NavigationStack);
                        stack.AddRange(Shell.Current.Navigation.ModalStack);
                    }
                }
                if (!PopToRoot)
                {
                    if(navigationsParams != null)
                    {
                        await Shell.Current.GoToAsync(pUrl, false, navigationsParams);
                    } else {
                        await Shell.Current.GoToAsync(pUrl, false);
                    }
                }
                if (PopToRootBeforeNavigating || PopToRoot)
                {
                    if(DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        foreach (var item in stack)
                        {
                            if(item != null)
                            {
                                Shell.Current.Navigation.RemovePage(item);
                            }
                        }
                    }
                }
            }
            _currentPage = Shell.Current.CurrentPage;
            return canNavigateFrom;
        }
    }
}
