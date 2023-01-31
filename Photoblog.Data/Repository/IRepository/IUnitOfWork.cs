using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoblog.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
	//I<ModelName>Repository ModelName { get; }
        IPhotoblogEntryRepository PhotoblogEntry { get; }
        IPhotoRepository Photo { get; }
        void Save();
    }
}
