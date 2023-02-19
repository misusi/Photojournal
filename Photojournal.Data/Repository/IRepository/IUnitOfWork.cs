using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photojournal.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
	//I<ModelName>Repository ModelName { get; }
        IJournalEntryRepository JournalEntry { get; }
        IPhotoRepository Photo { get; }
        void Save();
    }
}
