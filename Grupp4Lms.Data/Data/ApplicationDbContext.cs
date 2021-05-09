using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grupp4Lms.Core.Entities;

namespace Grupp4Lms.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Amne> Amne { get; set; }
        public DbSet<Forfattare> Forfattar { get; set; }
        public DbSet<Litteratur> Litteratur { get; set; }
        public DbSet<Niva> Niva { get; set; }

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}