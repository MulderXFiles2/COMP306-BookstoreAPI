namespace BookstoreApi.Dtos.Books

/*This DTO represents the data required to create a new book. 
 * 
 */
{
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int PublisherId { get; set; }
        public int PublicationYear { get; set; }
    }
}
