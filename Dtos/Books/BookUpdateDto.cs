namespace BookstoreApi.Dtos.Books
{

    /* This is the data required to update an existing book in the system.
     * It is the same as the BookCreateDto, as all fields can be updated.
     */
    public class BookUpdateDto
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
