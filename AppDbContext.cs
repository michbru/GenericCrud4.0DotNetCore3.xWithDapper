using GenericCrudApiDapper.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<tbl_contacts> tbl_contacts { get; set; }
        public DbSet<tblBook> tblBook { get; set; }
        public DbSet<tbl_employees> tbl_employees { get; set; }
        public DbSet<tbl_products> tbl_products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
