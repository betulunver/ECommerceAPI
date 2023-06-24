using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository; 
        private readonly ICustomerWriteRepository _customerWriteRepository;


        public ProductsController(
            IProductWriteRepository productWriteRepository, 
            IProductReadRepository productReadRepository, 
            IOrderWriteRepository orderWriteRepository, 
            ICustomerWriteRepository customerWriteRepository)
        {                                                                                                                    
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
        }
        [HttpGet]
        public async Task Get()
        {
            var customerId= Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new Customer() { Id = customerId, Name = "Betül" });

            await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla", Address = "İstanbul,Beylikdüzü" ,CustomerId=customerId});
            await _orderWriteRepository.AddAsync(new() { Description = "bla bla", Address = "İstanbul,Bahçelievler", CustomerId = customerId });

            await _productWriteRepository.AddAsync(new() { Id = Guid.NewGuid(), Name = "Tablet", Price = 100f, Stock = 25 });
            //Product p = await _productReadRepository.GetByIdAsync("c2d530f7-6933-4807-b133-0ea39f5ab69c");
            //p.Name = "Bilgisayar";
            await _orderWriteRepository.SaveAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
