using EIMS.Application.DTOs.Products;
using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateProductAsync(CreateProductRequest request);
        Task<Product> UpdateAsync(int id, UpdateProductRequest request);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<object>> GetAllCategoryNamesAsync();
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

    }
}
