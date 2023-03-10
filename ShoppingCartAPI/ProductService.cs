using ShoppingCartAPI.Data;
using ShoppingCartAPI.Services;

namespace ShoppingCartAPI;

public class ProductService
{

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    public IEnumerable<ProductDTO> GetAvailableProducts()
    {
        var products = _productRepository.GetAll();
        var dtos = new List<ProductDTO>();
        
        foreach (var p in products)
        {
            dtos.Add(new ProductDTO(p.Name, p.FinalPrice));
        }

        return dtos;
    }
}