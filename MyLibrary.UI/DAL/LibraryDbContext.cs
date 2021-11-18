using Microsoft.EntityFrameworkCore;
using MyLibrary.Lib.Models;
using System;

namespace MyLibrary.UI.DAL

    // cambiar Subjet y Enrollement

{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BooksCopies { get; set; }
        public DbSet<Loan> Loans { get; set; }



        public LibraryDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder Builder)
        {          
            Builder.Entity<Librarian>().ToTable("librarians");
            Builder.Entity<Client>().ToTable("clients");
            Builder.Entity<Book>().ToTable("books");
            Builder.Entity<BookCopy>().ToTable("bookscopies");
            Builder.Entity<Loan>().ToTable("loans");

            #region Relations

            Builder.Entity<Loan>()
                .HasOne(e => e.Client)
                .WithMany(s => s.Loans)
                .HasForeignKey(e => e.ClientId);

            Builder.Entity<Loan>()
                .HasOne(e => e.BookCopy)
                .WithMany(s => s.Loans)
                .HasForeignKey(e => e.BookCopyId);

            Builder.Entity<BookCopy>()
                .HasOne(e => e.Book)
                .WithMany(s => s.BookCopies)
                .HasForeignKey(e => e.BookId);

            #endregion

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=LAPTOP-TSPE80VL;Database=Academy;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder
                .UseMySql(connectionString: @"server=localhost;database=library;uid=libraryAdmin;password=1234;",
                new MySqlServerVersion(new Version(8, 0, 23)));
        }
    }
}
