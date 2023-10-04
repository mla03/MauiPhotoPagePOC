using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Pages
{
    public static class PageServiceConfigurator
    {
        public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
        {

            builder.Services.AddSingleton<JobPage>();
            builder.Services.AddSingleton<PhotosPage>();
            return builder;
        }
    }
}
