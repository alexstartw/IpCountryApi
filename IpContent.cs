using System.Text.Json.Serialization;

namespace CSharpSKA;

public class IpContent
{
    [JsonPropertyName("ip")]
    public string Ip { get; set; }
}