using Photoblog.Data.Repository.IRepository;
using Photoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoblog.Data.Repository
{
    public class PhotoblogEntryRepository: Repository<PhotoblogEntry>, IPhotoblogEntryRepository
    {
        private ApplicationDbContext _db;
        public PhotoblogEntryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(PhotoblogEntry obj)
        {
            _db.PhotoblogEntries.Update(obj);
        }
    }
}
