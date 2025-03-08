using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWordProcessorDBEFCore.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WordDatabase;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasKey(w => w.Id);
        }

        public void EnsureDatabaseCreated()
        {
            if (!Database.CanConnect())
            {
                Database.EnsureCreated(); // Создаёт БД и таблицы, если не существуют
            }
        }
    }
}
