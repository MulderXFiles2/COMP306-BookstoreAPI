namespace BookstoreApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderMethod { get; set; }
        public string Status { get; set; }

        public Book? Book { get; set; }
    }
}
