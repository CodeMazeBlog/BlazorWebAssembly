using Entities.Models;

namespace BlazorProducts.Server.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(Guid id);
    }
}
