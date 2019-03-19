using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IGI_1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Autors> Autors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
