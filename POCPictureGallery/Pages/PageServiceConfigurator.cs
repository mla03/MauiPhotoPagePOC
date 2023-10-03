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

            builder.Services.AddTransient<JobPage>();
            builder.Services.AddTransient<PhotosPage>();
            return builder;
        }
    }
}
