using BookstoreApi.Data;
using BookstoreApi.Models;
using Microsoft.EntityFrameworkCore;

/*  This is the Repository. It contains all the methods that the controllers call to get, add, update, and delete for all 3 entities: Books, Publishers, and Orders.
 * 
 */

namespace BookstoreApi.Repositories
{
    public class BookstoreRepository : IBookstoreRepository
    {
        private readonly BookstoreContext _context;

        public BookstoreRepository(BookstoreContext context)
        {
            _context = context;
        }

        // Books
        public Task<List<Book>> GetAllBooksAsync()
        {
            return _context.Books
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public Task<Book?> GetBookByIdAsync(int id)
        {
            return _context.Books
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            return Task.CompletedTask;
        }

        public Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            return Task.CompletedTask;
        }

        // Publishers
        public Task<List<Publisher>> GetAllPublishersAsync()
        {
            return _context.Publishers.ToListAsync();
        }

        public Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            return _context.Publishers.FirstOrDefaultAsync(p => p.PublisherId == id);
        }

        public async Task AddPublisherAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
        }

        public Task UpdatePublisherAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            return Task.CompletedTask;
        }

        public Task DeletePublisherAsync(Publisher publisher)
        {
            _context.Publishers.Remove(publisher);
            return Task.CompletedTask;
        }

        // Orders
        public Task<List<Order>> GetAllOrdersAsync()
        {
            return _context.Orders
                .Include(o => o.Book)
                .ToListAsync();
        }

        public Task<Order?> GetOrderByIdAsync(int id)
        {
            return _context.Orders
                .Include(o => o.Book)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask;
        }

        public Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            return Task.CompletedTask;
        }

        // Save changes to DB
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
