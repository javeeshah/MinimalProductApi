using Microsoft.EntityFrameworkCore;
using MinimalProductApi.Dtos;
using MinimalProductApi.Entities;

namespace MinimalProductApi.DbContexts
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task DeleteProductAsync(int id)
        {
            if (id == 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Product Id is not valid");

            var product = _productDbContext.Products.Single(p => p.Id == id);

            _productDbContext.Products.Remove(product);
            await _productDbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            if (!_productDbContext.Products.Any(x => x.Id == id))
                return null;

            return _productDbContext.Products.First(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        public async Task SaveProductAsync(ProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto));

            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _productDbContext.Products.AddAsync(product);
            await _productDbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            if (id == 0 || !_productDbContext.Products.Any(p => p.Id == id) || id != productDto.Id)
                throw new ArgumentException(nameof(id), "No product exist in the database for the given Id");

            var product = await _productDbContext.Products.SingleAsync(p => p.Id == id);
            product.Name = productDto.Name;
            product.Price = productDto.Price;

            _productDbContext.Products.Update(product);
            await _productDbContext.SaveChangesAsync();
        }
    }
}
