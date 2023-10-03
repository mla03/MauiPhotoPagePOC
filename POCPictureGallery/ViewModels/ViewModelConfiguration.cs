﻿using POCPictureGallery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.ViewModels
{
    public static class ViewModelConfiguration
    {
        public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<JobViewModel>();
            builder.Services.AddTransient<PhotosViewModel>();

            return builder;
        }
    }
}
