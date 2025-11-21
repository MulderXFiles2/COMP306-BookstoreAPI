using AutoMapper;
using BookstoreApi.Dtos.Orders;
using BookstoreApi.Models;
using BookstoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IBookstoreRepository _repository;
        private readonly IMapper _mapper;

        public OrdersController(IBookstoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET a list of orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _repository.GetAllOrdersAsync();
            var dto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(dto);
        }

        // GET an order by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            var dto = _mapper.Map<OrderDto>(order);
            return Ok(dto);
        }

        // POST , get a new order
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderCreateDto createDto)
        {
            var order = _mapper.Map<Order>(createDto);

            await _repository.AddOrderAsync(order);
            await _repository.SaveChangesAsync();

            var dto = _mapper.Map<OrderDto>(order);
            return Ok(dto);
        }

        // PUT replace an existing order
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutOrder(int id, OrderUpdateDto updateDto)
        {
            var existing = await _repository.GetOrderByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(updateDto, existing);

            await _repository.UpdateOrderAsync(existing);
            await _repository.SaveChangesAsync();

            var dto = _mapper.Map<OrderDto>(existing);
            return Ok(dto);
        }

        // PATCH , update part of an existing order
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchOrder(int id, OrderUpdateDto patchDto)
        {
            var existing = await _repository.GetOrderByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(patchDto, existing);

            await _repository.UpdateOrderAsync(existing);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE an order
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var existing = await _repository.GetOrderByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.DeleteOrderAsync(existing);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
