namespace MarketplaceBff.Extensions;

public static class StringExtensions
{
    public static byte[] ConvertToBytes(this string base64String)
    {
        var base64Data = base64String.Substring(base64String.IndexOf(',') + 1);

        return Convert.FromBase64String(base64Data);
    }
    
    public static string AddImageExtension(this string image)
    {
        return "data:image/jpeg;base64," + image;
    }
}