using LibraryApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Utility
{
    public class LibraryDbContext:IdentityDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentOrder> RentOrders { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    }
}
