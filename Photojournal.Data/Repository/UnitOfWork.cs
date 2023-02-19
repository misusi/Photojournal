using Photojournal.Data;
using Photojournal.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photojournal.Models;

namespace Photojournal.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            JournalEntry = new JournalEntryRepository(_db);
            Photo = new PhotoRepository(_db);
        }

        public IJournalEntryRepository JournalEntry { get; private set; }
        public IPhotoRepository Photo { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
