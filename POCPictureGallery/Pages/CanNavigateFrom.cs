using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Pages
{
    public interface CanNavigateFrom
    {
        Task<bool> CanNavigateFrom();
    }
}
