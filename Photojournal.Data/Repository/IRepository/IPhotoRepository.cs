using Photoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoblog.Data.Repository.IRepository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void Update(Photo obj);
    }
}
