using Application;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Converter;
using ProductService.Api.Resource;
using ProductService.Domain.Model;

namespace ProductService.Api.Controller;

[ApiController]
[Route("/api")]
public class ProductController(IProductService productService)
{
   // [HttpGet("all-products")]
   // public ActionResult<Product> GetProducts()
   // {
   //    return new ActionResult<Product>(new Product("T"));
   // }

   [HttpPost("create-product")]
   [Produces("application/json")]
   public ActionResult<Guid> Create([FromBody] ProductResource resource)
   {
      return productService.CreateProduct(ProductConverter.ToModel(resource));
   }

   [HttpGet("products/{id}")]
   [Produces("application/json")]
   public ActionResult<Product> GetProduct(Guid id)
   {
      var product = productService.GetProductById(id);
      if (product == null)
      {
         return new NotFoundResult();
      }

      return product;
   }

   [HttpPut("products/{id}")]
   [Produces("application/json")]
   public ActionResult<Guid> UpdateProduct(Guid id, [FromBody] ProductResource resource)
   {
      var product = ProductConverter.ToModel(resource);
      product.Id = id;
      productService.Update(product);
   }
}