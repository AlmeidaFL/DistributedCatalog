using Grpc.Net.Client;

namespace CatalogBff.Integration;

public interface IGrpcClient<out T> : IDisposable
{
    T? Create();
}