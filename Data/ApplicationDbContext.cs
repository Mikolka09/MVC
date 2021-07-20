using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.Tracked += ChangeTracker_Tracked;
        }

        private void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            if(e.Entry.Entity is Book book && book.Codes == null)
            {
                book.Codes = new HashSet<string>();
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>(b =>
            {
                b.Property(e => e.Codes).HasConversion<string>(
                    set => System.Text.Json.JsonSerializer.Serialize(set, null),
                    str => System.Text.Json.JsonSerializer.Deserialize<HashSet<string>>(str, null));
            });
        }

        
        public DbSet<Publisher> Publisher { get; set; }
    }
}
