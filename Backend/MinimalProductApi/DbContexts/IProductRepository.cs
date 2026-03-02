using MinimalProductApi.Dtos;
using MinimalProductApi.Entities;

namespace MinimalProductApi.DbContexts
{
    public interface IProductRepository
    {
        Task SaveProductAsync(ProductDto productDto);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task UpdateProductAsync(int id, ProductDto productDto);

        Task DeleteProductAsync(int id);
    }
}
