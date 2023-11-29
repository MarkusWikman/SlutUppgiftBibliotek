﻿using Microsoft.EntityFrameworkCore;
using SlutUppgiftBibliotek.Models;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using EntityFrameworkCore.EncryptColumn.Extension;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);
        }
        private readonly IEncryptionProvider _provider;
        public Context()
        {
            this._provider = new GenerateEncryptionProvider("A2b9RfL7kP3dQ1mT6yZ8iX5vN0cU4hGs");
        }
    }
}
