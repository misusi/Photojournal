using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Photojournal.Models;

namespace Photojournal.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    //public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
