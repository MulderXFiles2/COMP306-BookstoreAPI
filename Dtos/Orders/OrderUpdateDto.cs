namespace BookstoreApi.Dtos.Orders
{
    public class OrderUpdateDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
