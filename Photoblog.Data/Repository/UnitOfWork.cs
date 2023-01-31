using Photoblog.Data;
using Photoblog.Data.Repository.IRepository;
//using Photoblog.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photoblog.Models;

namespace Photoblog.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            PhotoblogEntry = new PhotoblogEntryRepository(_db);
            Photo = new PhotoRepository(_db);
            //Category = new CategoryRepository(_db);
            //ApplicationUser = new ApplicationUserRepository(_db);
        }

        //public ICategoryRepository Category { get; private set; }
        //public IApplicationUserRepository ApplicationUser { get; private set; }
        public IPhotoblogEntryRepository PhotoblogEntry { get; private set; }
        public IPhotoRepository Photo { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
