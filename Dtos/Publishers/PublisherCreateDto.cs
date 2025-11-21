namespace BookstoreApi.Dtos.Publishers
{

/*This DTO represents the data required to create a new publisher. 
* 
*/
    public class PublisherCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
