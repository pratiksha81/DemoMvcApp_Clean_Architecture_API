using Microsoft.AspNetCore.Mvc;
using DemoMvcApp.Services;
using DemoMvcApp.Models;
using System;
using DemoMvcApp.Handler;
using DemoMvcApp.command;
using DemoMvcApp.Mapper;
using DemoMvcApp.Infrastructure;
using DemoMvcApp.DTOs;
using MediatR;

namespace DemoMvcApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IErrorHandlingService<string> _errorHandlingService;
        private readonly CreateProductCommandHandler _commandHandler;
        private readonly IMapper _mapper;//mapper
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;



        // Dependency Injection
        public ProductsController(IProductService productService, IErrorHandlingService<string> errorHandlingService, CreateProductCommandHandler commandHandler, IMapper mapper, IProductRepository productRepository, IMediator mediator)
        {
            _productService = productService;

            _errorHandlingService = errorHandlingService;
            _commandHandler = commandHandler;
            _productRepository = productRepository;

            _mapper = mapper;
            _mediator = mediator;


        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception)
            {
                var errorMessage = _errorHandlingService.GetError();
                return StatusCode(500, errorMessage);
            }
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                return Ok(product);
            }
            catch (Exception)
            {
                var errorMessage = _errorHandlingService.GetError();
                return StatusCode(500, errorMessage);
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductDto productDto, IFormFile productImage)
        {
            try
            {
                if (productImage != null && productImage.Length > 0)
                {
                    // Define the path to save the image
                    var filePath = Path.Combine("C:\\Users\\ecs\\source\\test clean architecture\\Infrastructure\\Image", productImage.FileName);

                    // Save the file to the defined path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productImage.CopyToAsync(stream);
                    }

                    // Map the DTO to the course entity
                    var product = _mapper.MapToEntity(productDto);

                    // Assign the image path to the entity
                    product.ProductImage = filePath;

                    // Add course to the service (or DB)
                    _productService.AddProduct(product);

                    return Ok(new { message = "Products and image added successfully" });
                }
                else
                {
                    return BadRequest("Product image is required.");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = _errorHandlingService.GetError();
                return StatusCode(500, errorMessage);
            }
        }



        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile(IFormFile imageFile, string Name, decimal Price)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\ecs\\source\\test clean architecture\\Infrastructure\\Image", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Save product info to the database
                var command = new CreateProductCommand
                {
                    Name = Name,
                    Price = Price,
                    ProductImage = fileName
                   /* ImagePath = filePath*/
                };

               await _commandHandler.Handle(command, HttpContext.RequestAborted);
                return Ok(new { message = "File uploaded successfully"/*, imagePath = filePath*/ });
            }
            return BadRequest(new { message = "No file uploaded" });
        }


        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] ProductDto productDto, IFormFile? productImage)
        {
            try
            {
                // Fetch the existing product by ID
                var existingProduct = _productService.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound(new { message = "Product not found" });
                }

                // Check if an image file is provided
                if (productImage != null && productImage.Length > 0)
                {
                    // Define the path to save the image
                    var filePath = Path.Combine("C:\\Users\\ecs\\source\\test clean architecture\\Infrastructure\\Image", productImage.FileName);

                    // Save the new file to the defined path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productImage.CopyToAsync(stream);
                    }

                    // Update the image path in the product entity
                    existingProduct.ProductImage = filePath;
                }

                // Map other properties from DTO to the product entity
                existingProduct = _mapper.MapToEntity(productDto);

                // Update the product in the database
                _productService.UpdateProduct(id, existingProduct);

                return Ok(new { message = "Product updated successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception and return a custom error message
                var errorMessage = _errorHandlingService.GetError();
                return StatusCode(500, errorMessage);
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception)
            {
                var errorMessage = _errorHandlingService.GetError();
                return StatusCode(500, errorMessage);
            }
        }


        //MediatR
        [HttpPost("MediatR")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
        }







        // mapper 

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product =  _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            var productDto = _mapper.MapToDto(product);
            return Ok(productDto);
        }

        [HttpPost("mapper")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            var product = _mapper.MapToEntity(productDto);
             _productRepository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
        }

    }
}
