using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Model
{
    public class Job : BaseModel<Job>
    {
        public string Guid { get; set; }

        [Ignore]
        public List<Photo> Documents { get; set; }
    }
}
