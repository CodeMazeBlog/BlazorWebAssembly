using BlazorProducts.Server.Paging;
using Entities.Models;
using Entities.RequestFeatures;

namespace BlazorProducts.Server.Repository
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetProducts(ProductParameters productParameters);
        Task<Product> GetProduct(Guid id);
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product, Product dbProduct);
        Task DeleteProduct(Product product);
    }
}
