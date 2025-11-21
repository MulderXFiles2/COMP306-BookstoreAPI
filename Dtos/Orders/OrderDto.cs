namespace BookstoreApi.Dtos.Orders
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public string? BookTitle { get; set; }
    }
}
