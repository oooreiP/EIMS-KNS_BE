using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Products;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync(includeProperties: "Category");
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _unitOfWork.ProductRepository.GetByCodeAsync(code);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.ProductRepository.GetByCategoryAsync(categoryId);
        }
        public async Task<Product> CreateProductAsync(CreateProductRequest request)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryID);
            if (category == null)
                throw new Exception("Không tìm thấy nhóm hàng hóa/dịch vụ.");

            var product = new Product
            {
                Code = request.Code,
                Name = request.Name,
                CategoryID = request.CategoryID,
                Unit = request.Unit,
                BasePrice = request.BasePrice,
                VATRate = request.VATRate,
                Description = request.Description,
                IsActive = request.IsActive
            };

            await _unitOfWork.ProductRepository.CreateAsync(product);
            await _unitOfWork.SaveChanges();
            return product;
        }
        public async Task<Product> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id)
                          ?? throw new Exception("Product not found.");
            product.Code = request.Code ?? product.Code;
            product.Name = request.Name ?? product.Name;
            product.Unit = request.Unit ?? product.Unit;
            product.BasePrice = request.BasePrice ?? product.BasePrice;
            product.VATRate = request.VATRate ?? product.VATRate;
            product.Description = request.Description ?? product.Description;
            product.CategoryID = request.CategoryID ?? product.CategoryID;
            product.IsActive = request.IsActive ?? product.IsActive;

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChanges();

            return product;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }
        public async Task<IEnumerable<object>> GetAllCategoryNamesAsync()
        {
            return await _unitOfWork.CategoryRepository
                .GetAllQueryable()                   
                .Where(c => c.IsActive == true)
                .Select(c => new { c.CategoryID, c.Name })
                .ToListAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return false;

            await _unitOfWork.ProductRepository.DeleteAsync(product);
            await _unitOfWork.SaveChanges();
            return true;
        }
    }
}
