using Microsoft.EntityFrameworkCore;

namespace SlutUppgiftBibliotek.Data
{
    internal class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=NewtonLibraryMarkus; Trusted_Connection=True; Trust Server Certificate =Yes; User Id=NewtonLibrary; password=NewtonLibrary");
        }
    }
}
