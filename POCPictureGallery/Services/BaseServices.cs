using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Services
{
    public class BaseService<T>
    {
        public static T GetInstance()
        {
            return ServicesProvider.GetService<T>();
        }
    }
}
