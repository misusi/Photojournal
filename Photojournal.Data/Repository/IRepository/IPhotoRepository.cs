using Photojournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photojournal.Data.Repository.IRepository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void Update(Photo obj);
    }
}
