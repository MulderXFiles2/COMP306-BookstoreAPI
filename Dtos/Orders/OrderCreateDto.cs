namespace BookstoreApi.Dtos.Orders
{

 /*This DTO represents the data required to create a new book. 
 * 
 */
    public class OrderCreateDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
