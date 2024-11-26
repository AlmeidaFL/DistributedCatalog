using CatalogService.Domain;
using CatalogService.Persistence;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcContracts;
using Image = CatalogService.Domain.Image;
using Product = GrpcContracts.Product;

namespace CatalogService.Services;

public class CatalogService(IProductRepository repository)
    : GrpcContracts.CatalogService.CatalogServiceBase
{
    public override async Task GetProductsBy(
        SearchTerm request,
        IServerStreamWriter<Product> responseStream,
        ServerCallContext context)
    {
        var productsCursor = await repository.GetProductsBySearchTerm(request.Value);

        while (await productsCursor.MoveNextAsync())
        {
            foreach (var product in productsCursor.Current)
            {
                await responseStream.WriteAsync(ToResource(product));
            }
        }
    }

    public override async Task<Product> GetProductById(GetProductByIdMessage request, ServerCallContext context)
    {
        var product = await repository.GetProductById(request.ProductId);

        return product == null ? null! : ToResource(product)!;
    }
    
    public override async Task<ProductWithoutImage> GetProductHeaderById(
        GetProductByIdMessage request,
        ServerCallContext context)
    {
        var product = await repository.GetProductById(request.ProductId);

        return product == null ? null! : ToResourceHeader(product)!;
    }

    public override async Task GetAllProducts
        (Empty request,
        IServerStreamWriter<Product> responseStream,
        ServerCallContext context)
    {
        var productsCursor = await repository.GetAllProducts();

        while (await productsCursor.MoveNextAsync())
        {
            foreach (var product in productsCursor.Current)
            {
                await responseStream.WriteAsync(ToResource(product));
            }
        }
    }

    public override async Task GetProductsByIds(
        GetProductsByIdsMessage request,
        IServerStreamWriter<Product> responseStream,
        ServerCallContext context)
    {
        var productsCursor = await repository.GetProductsByIdsAsync(request.ProductId);

        while (await productsCursor.MoveNextAsync())
        {
            foreach (var product in productsCursor.Current)
            {
                await responseStream.WriteAsync(ToResource(product));
            }
        }
    }
    
    public override async Task GetProductsByIdsWithoutImage(
        GetProductsByIdsMessage request,
        IServerStreamWriter<ProductWithoutImage> responseStream,
        ServerCallContext context)
    {
        var productsCursor = await repository.GetProductsByIdsAsync(request.ProductId);

        while (await productsCursor.MoveNextAsync())
        {
            foreach (var product in productsCursor.Current)
            {
                await responseStream.WriteAsync(ToResourceHeader(product));
            }
        }
    }

    private ProductWithoutImage ToResourceHeader(Domain.Product product)
    {
        return new ProductWithoutImage()
        {
            Id = product.Id,
            Categories = { product.Categories },
            Description = product.Description,
            Name = product.Name,
            Price = (double)product.Price,
            VendorId = product.VendorId.ToString(),
            StockQuantity = product.StockQuantity,
        };
    }
    
    private static Product ToResource(Domain.Product product)
    {
        return new Product
        {
            Id = product.Id,
            Categories = { product.Categories },
            Description = product.Description,
            VendorId = product.VendorId.ToString(),
            StockQuantity = product.StockQuantity,
            Name = product.Name,
            Price = (double)product.Price,
            Image = product.Images.Count > 0
                ? new GrpcContracts.Image() { Data = ByteString.CopyFrom(product.Images[0].Representation) }
                : null,
        };
    }
    
    public override async Task<Empty> AddProduct(
        IAsyncStreamReader<Product> requestStream,
        ServerCallContext context)
    {
        await foreach (var resource in requestStream.ReadAllAsync())
        {
            var product = new Domain.Product()
            {
                VendorId = Guid.Parse(resource.VendorId),
                Name = resource.Name,
                Description = resource.Description,
                Price = (decimal)resource.Price,
                Categories = resource.Categories.ToArray(),
                StockQuantity = resource.StockQuantity,
                Images = 
                [
                    new Image
                    {
                        Representation = resource.Image.Data.ToByteArray(),
                    }
                ]
            };
            await repository.AddProduct(product);
        }
        
        return await Task.FromResult(new Empty());
    }
    
    public override async Task<ReservedResult> ReserveProducts(ReserveProductsMessage request, ServerCallContext context)
    {
        try
        {
            await ReserveProducts(
                request.CustomerId,
                request.ReservedProducts.Select(r => new Reservation()
                {
                    ProductId = r.ProductId,
                    Quantity = r.Quantity,
                }).ToList());
            
            return new ReservedResult { Message = "Reserved successfully" };
        }
        catch (Exception ex)
        {
            return new ReservedResult { Message = ex.Message, IsError = true};
        }
    }
    
    // Probably this isn't supporting concurrency reservations
    private async Task ReserveProducts(string customerId, IReadOnlyList<Reservation> reservations)
    {
        var products = (await repository.GetProductsByIds(reservations.Select(r => r.ProductId)))
            .ToList();

        if (!HasStockForReserve(out var productId))
        {
            throw new Exception($"No stock available for reservation for product {productId}");
        };
        
        foreach (var product in products)
        {
            var productToBeReserved = reservations.First(r => r.ProductId == product.Id);
            product.ReservationByCustomerId[customerId] = productToBeReserved;
        }
            
        await repository.UpdateProducts(products);


        bool HasStockForReserve(out string? productId)
        {
            productId = null;
            
            foreach (var product in products)
            {
                if (product.QuantityReserved > reservations.First(r => r.ProductId == product.Id).Quantity)
                {
                    productId = product.Id;
                    return false;
                }
            }

            return true;
        }
    }
    
    public override async Task<ReservedResult> UnreserveProducts(CustomerId request, ServerCallContext context)
    {
        try
        {
            await UnreserveProducts(request.CustomerId_);
            
            return new ReservedResult { Message = "Reserved successfully" };
        }
        catch (Exception ex)
        {
            return new ReservedResult { Message = ex.Message, IsError = true};
        }
    }
    
    // Probably this isn't supporting concurrency reservations
    private async Task UnreserveProducts(string customerId)
    {
        var products = await repository.GetProductsReservedByCustomer(customerId);
        
        foreach (var product in products)
        {
            product.ReservationByCustomerId.Remove(customerId);
        }
            
        await repository.UpdateProducts(products);
    }
}