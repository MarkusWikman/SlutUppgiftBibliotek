using Microsoft.EntityFrameworkCore;
using SlutUppgiftBibliotek.Models;

namespace SlutUppgiftBibliotek.Data
{
    internal class Context : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<LoanCard> LoanCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=NewtonLibraryMarkus; Trusted_Connection=True; Trust Server Certificate =Yes; User Id=NewtonLibrary; password=NewtonLibrary");
        }
    }
}
