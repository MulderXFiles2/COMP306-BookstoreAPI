namespace BookstoreApi.Dtos.Books
{

    /* This is the full book data the API returns when someone requests a book. 
     * The API is the middleman between the frontend and the database, what programs use to communicate with each other.
     */
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int PublisherId { get; set; }
        public int PublicationYear { get; set; }

        public string? PublisherName { get; set; }
    }
}
