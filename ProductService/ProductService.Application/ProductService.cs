using ProductService.Domain.Model;
using ProductService.Integration;

namespace Application;

public class ProductService(IProductRepository productRepository)
   : IProductService
{
   public Guid CreateProduct(Product product)
   {
      try
      {
         var id = Guid.NewGuid(); 
         product.Id = id;
         productRepository.SaveProduct(product);

         return id;
      }
      catch (Exception ex)
      {
         
      }

      return new Guid();
   }

   public Product? GetProductById(Guid id)
   {
      try
      {
         return productRepository.GetProductById(id);
      }
      catch
      {
         
      }

      return null;
   }

   public IReadOnlyList<Product> GetVendorCatalogById(Guid id)
   {
      return productRepository.GetCatalogByVendorId(id);
   }

   public void Update(Product product)
   {
      productRepository.Update(product);
   }
}
