using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogService.Domain;

public record Image
{
    public string FileName { get; init; }
    public byte[] Representation { get; init; }
}