using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Product_Order_API.Data;
using Online_Product_Order_API.Models;

namespace Online_Product_Order_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Order> _validator;
        public OrderController(AppDbContext context , IValidator<Order> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> getAllOrder()
        {
            var orders = await _context.orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getOrderByID(int id)
        {
            var order = await _context.orders.FindAsync(id);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteOrder(int id)
        {
            var existOrder = await _context.orders.FindAsync(id);
            if (existOrder == null) return BadRequest(new { message = "Order not found" });

            _context.orders.Remove(existOrder);
            await _context.SaveChangesAsync();
            return Ok(new {message="Order deleted..."});
        }

        [HttpPost]
        public async Task<IActionResult> addOrder(Order order)
        {
            var result = await _validator.ValidateAsync(order);
            if (!result.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = result.Errors.Select(e => e.ErrorMessage)
                });
            }
            var newOrder = new Order
            {   
                orderID = order.orderID,
                customerName = order.customerName,
                productName = order.productName,
                quantity = order.quantity,
                price = order.price,
                orderdate = order.orderdate,
            };

            _context.orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Order added..." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateOrder(int id, Order order)
        {
            var result = await _validator.ValidateAsync(order);
            if (!result.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = result.Errors.Select(e => e.ErrorMessage)
                });
            }
            var existOrder = await _context.orders.FindAsync(id);
            if (existOrder == null) return BadRequest(new { message = "Order not found..." });

            existOrder.orderID = order.orderID;
            existOrder.customerName = order.customerName;
            existOrder.productName = order.productName;
            existOrder.quantity = order.quantity;
            existOrder.price = order.price;
            existOrder.orderdate = order.orderdate; 

            await _context.SaveChangesAsync();
            return Ok(new { message = "Order updated..." });
        }
    }
}
