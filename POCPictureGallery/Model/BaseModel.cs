using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Model
{
    public class BaseModel<T>
    {
        [Ignore]
        public bool IsDirty { get; set; }

        public void MarkDirty()
        {
            IsDirty = true;
        }

        public void MarkPrestine()
        {
            IsDirty = false;
        }
    }
}
