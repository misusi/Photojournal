using Photojournal.Data.Repository.IRepository;
using Photojournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photojournal.Data.Repository
{
    public class JournalEntryRepository: Repository<JournalEntry>, IJournalEntryRepository
    {
        private ApplicationDbContext _db;
        public JournalEntryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(JournalEntry obj)
        {
            _db.JournalEntries.Update(obj);
        }
    }
}
