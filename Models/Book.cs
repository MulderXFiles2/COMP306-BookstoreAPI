namespace BookstoreApi.Models
{

    /* This is the Book model that EF Core turns into a row in the database
     * This is different from the BookDto, which just the data the user sends when creating a book. 
     */
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int PublisherId { get; set; }
        public int PublicationYear { get; set; }

        public Publisher? Publisher { get; set; }
    }
}
