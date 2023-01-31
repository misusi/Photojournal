//using Photoblog.Models;
using Microsoft.EntityFrameworkCore;
using Photoblog.Models;

namespace Photoblog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
