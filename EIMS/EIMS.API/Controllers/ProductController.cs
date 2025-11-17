using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Products;
using EIMS.Application.Features.Products.Commands;
using EIMS.Infrastructure.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ISender _sender;

        public ProductController(IProductService productService, ISender sender)
        {
            _productService = productService;
            _sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound("Product not found.");
            return Ok(product);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            try
            {
                var product = await _productService.CreateProductAsync(request);
                return Ok(new { message = "Thêm hàng hóa thành công!", data = product });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var updated = await _productService.UpdateAsync(id, request);
            return Ok(new { message = "Updated successfully", data = updated });
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("categories_names")]
        public async Task<IActionResult> GetCategoryNames()
        {
            var names = await _productService.GetAllCategoryNamesAsync();
            return Ok(names);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound("Product not found.");
            return Ok(new { message = "Deleted successfully" });
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            return Ok(products);
        }
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] bool isActive)
        {
            var command = new UpdateProductStatusCommand
            {
                ProductID = id,
                IsActive = isActive
            };

            var result = await _sender.Send(command);

            if (result.IsFailed)
            {
                if (result.Errors.Any(e => e.Metadata.ContainsKey("ErrorCode") && (string)e.Metadata["ErrorCode"] == "Product.NotFound"))
                {
                    return NotFound(result.Errors.First().Message);
                }
                return BadRequest(result.Errors.First().Message);
            }

            return Ok(new { message = "Product status updated successfully." });
        }
    }
}

