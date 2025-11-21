using AutoMapper;
using BookstoreApi.Dtos.Books;
using BookstoreApi.Models;
using BookstoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;

/*The controller provides all six basic crud API Actions (get, get by id, post, put, patch, delete) for each entity: Books, Publishers, and Orders.
 * 
 */

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookstoreRepository _repository;
        private readonly IMapper _mapper;

        public BooksController(IBookstoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET all the books in the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _repository.GetAllBooksAsync();
            var dto = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(dto);
        }

        // GET a book by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            var dto = _mapper.Map<BookDto>(book);
            return Ok(dto);
        }

        // POST, create a new book
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(BookCreateDto createDto)
        {
            // Take incoming data and turn it into a book object to be saved in the database
            var book = _mapper.Map<Book>(createDto);

            await _repository.AddBookAsync(book);
            await _repository.SaveChangesAsync();

            // Take the book object and send it back to the user
            var dto = _mapper.Map<BookDto>(book);

            
            return Ok(dto);
        }

        // PUT, replace an existing book
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto updateDto)
        {
            var existing = await _repository.GetBookByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Take updated values from user and put them into existing book object
            _mapper.Map(updateDto, existing);

            await _repository.UpdateBookAsync(existing);
            await _repository.SaveChangesAsync();

            // No need to return anything, just a status code
            var dto = _mapper.Map<BookDto>(existing);
            return Ok(dto);
        }

        // PATCH, update a book object,  partially
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchBook(int id, BookUpdateDto patchDto)
        {
            //Get existing book from database
            var existing = await _repository.GetBookByIdAsync(id);
            if (existing == null)
                return NotFound();

            //Take whatever fields user sent and paste them into an existing book object
            _mapper.Map(patchDto, existing);

            await _repository.UpdateBookAsync(existing);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE, remove a book by id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existing = await _repository.GetBookByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.DeleteBookAsync(existing);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
