using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using MVC.Data;

namespace MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>(b =>
            {
                b.Property(e => e.Codes).HasConversion<string>(
                    array => System.Text.Json.JsonSerializer.Serialize(array, null),
                    str => System.Text.Json.JsonSerializer.Deserialize<string[]>(str, null));
            });
        }

        public DbSet<MVC.Data.Publisher> Publisher { get; set; }
    }
}
