using ProductService.Api.Resource;
using ProductService.Domain.Model;

namespace ProductService.Api.Converter;

internal static class ProductConverter
{
   internal static Product ToModel(ProductResource resource)
   {
      return new Product(
         Name: resource.Name,
         Description: resource.Description,
         Price: resource.Price,
         VendorId: resource.VendorId
      );
   } 
}