using Grpc.Net.Client;

using grpc = Grpc.Core;

namespace CatalogBff.Integration.Implementation;

public class GrpcClient<T>(string connectionUrl) : IGrpcClient<T>, IDisposable where T : grpc::ClientBase<T>
{
    private string ConnectionUrl { get; set; } = connectionUrl;
    private GrpcChannel Channel { get; set; } = null!;

    private GrpcChannel CreateChannel()
    {
        Channel = GrpcChannel.ForAddress(ConnectionUrl);
        return Channel;
    }

    public void Dispose()
    {
        Channel?.Dispose();   
    }

    public T? Create()   
    {
        return (T?)Activator.CreateInstance(typeof(T), CreateChannel());
    }
}