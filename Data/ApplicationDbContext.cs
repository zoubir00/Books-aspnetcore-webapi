﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using My_Books.Data.Models;
using System.Reflection.Emit;

namespace My_Books.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Book_Authors>()
                .HasOne(b => b.Book)
                .WithMany(ab => ab.Book_Authors)
                .HasForeignKey(bi => bi.bookId);


            modelBuilder.Entity<Book_Authors>()
               .HasOne(a => a.Author)
               .WithMany(ab => ab.Book_Authors)
               .HasForeignKey(ai => ai.AuthorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Authors> Book_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookFiles> BookFiles { get; set; }
    }
}
