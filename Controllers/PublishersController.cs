using AutoMapper;
using BookstoreApi.Dtos.Publishers;
using BookstoreApi.Models;
using BookstoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IBookstoreRepository _repository;
        private readonly IMapper _mapper;

        public PublishersController(IBookstoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET a list of all publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublishers()
        {
            var publishers = await _repository.GetAllPublishersAsync();
            var dto = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            return Ok(dto);
        }

        // GET a single publisher by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
        {
            var publisher = await _repository.GetPublisherByIdAsync(id);
            if (publisher == null)
                return NotFound();

            var dto = _mapper.Map<PublisherDto>(publisher);
            return Ok(dto);
        }

        // POST, create a new publisher
        [HttpPost]
        public async Task<ActionResult<PublisherDto>> PostPublisher(PublisherCreateDto createDto)
        {
            var publisher = _mapper.Map<Publisher>(createDto);

            await _repository.AddPublisherAsync(publisher);
            await _repository.SaveChangesAsync();

            var dto = _mapper.Map<PublisherDto>(publisher);
            return Ok(dto);
        }

        // PUT , replace an existing publisher with new data
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPublisher(int id, PublisherUpdateDto updateDto)
        {
            var existing = await _repository.GetPublisherByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(updateDto, existing);

            await _repository.UpdatePublisherAsync(existing);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // PATCH, update an existing publisher
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchPublisher(int id, PublisherUpdateDto patchDto)
        {
            var existing = await _repository.GetPublisherByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(patchDto, existing);

            await _repository.UpdatePublisherAsync(existing);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE, remove a publisher
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var existing = await _repository.GetPublisherByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.DeletePublisherAsync(existing);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
