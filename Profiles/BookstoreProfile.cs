using AutoMapper;
using BookstoreApi.Dtos.Books;
using BookstoreApi.Dtos.Orders;
using BookstoreApi.Dtos.Publishers;
using BookstoreApi.Models;

/* This tells AutoMapper how to convert between DTO's and Models
 * 
 * Eg; CreateMap tells AutoMapper to map between Book and BookDto
 * ForMember tells AutoMapper how to handle specific properties that need special handling
 * dest => dest.PublisherName specifies the destination property
 * opt => opt.MapFrom(...) specifies how to get the value for that property from the source object
 * This is needed for the first part, since when mapping from Book to BookDto, we need to get the Publisher's Name from the Publisher object
 * 
 * For publisher its easier, since there are no objects from another entity to consider, 
 * 
 * Order is similar to Book, since it needs to get the Book Title from the Book object
 */

namespace BookstoreApi.Profiles
{
    public class BookstoreProfile : Profile
    {
        public BookstoreProfile()
        {
           
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : null));

            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();

           
            CreateMap<Publisher, PublisherDto>();
            CreateMap<PublisherCreateDto, Publisher>();
            CreateMap<PublisherUpdateDto, Publisher>();

            
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : null));

            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();
        }
    }
}
