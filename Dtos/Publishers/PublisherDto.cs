namespace BookstoreApi.Dtos.Publishers
{
    public class PublisherDto
    {
        public int PublisherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
