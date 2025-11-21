using BookstoreApi.Models;

/*  This is the Repository Interface. It defines all the methods that the Repository class must implement.
 * 
 */

namespace BookstoreApi.Repositories
{
    public interface IBookstoreRepository
    {
        // Books
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);

        // Publishers
        Task<List<Publisher>> GetAllPublishersAsync();
        Task<Publisher?> GetPublisherByIdAsync(int id);
        Task AddPublisherAsync(Publisher publisher);
        Task UpdatePublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(Publisher publisher);

        // Orders
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);

        // Save changes to DB
        Task<bool> SaveChangesAsync();
    }
}
