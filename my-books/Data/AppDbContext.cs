﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;

namespace my_books.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Books_Authors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(a => a.Author)
                .WithMany(ba => ba.Books_Authors)
                .HasForeignKey(ai => ai.AuthorId);

            modelBuilder.Entity<Log>()
                .HasKey(n => n.Id);

            base.OnModelCreating(modelBuilder);
        }




        public DbSet<Book> Books { get; set; }  
        public DbSet<Publisher> Publishers { get; set; } 
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }


        public DbSet<Log> Logs { get; set; }
        //public DbSet<MyToken> MyTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
