using Microsoft.EntityFrameworkCore;
using BookstoreApi.Models;

/* This class represents the entire database
 * 
 */

namespace BookstoreApi.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
