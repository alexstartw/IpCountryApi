using System.Text.Json.Serialization;

namespace WebApplication3;

public class IpContent
{
    [JsonPropertyName("ip")]
    public string Ip { get; set; }
}