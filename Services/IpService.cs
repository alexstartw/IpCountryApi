using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpSKA;
using CSharpSKA.Controllers;
using CSharpSKA.Interface;

namespace CSharpSKA.Services;

public class IpService : IIpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<IpController> _logger;

    public IpService(HttpClient httpClient, ILogger<IpController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public IpContent GetIp()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add((new MediaTypeWithQualityHeaderValue("application/json")));
        var responseBody = _httpClient.GetAsync("https://api.ipify.org?format=json").Result.Content.ReadAsStreamAsync().Result;
        var deserializeResponseBody = JsonSerializer.Deserialize<IpContent>(responseBody);
        _logger.LogInformation("The response IP I get is : " + deserializeResponseBody.Ip);
        return deserializeResponseBody;
    }

    public string GetCountry()
    {
        var serializeResultOfGetIp = JsonSerializer.Serialize(new[] { GetIp()?.Ip });
        var dataForPost = new StringContent(serializeResultOfGetIp, Encoding.UTF8, "application/json");
        var responseBodyFromGetCountryCode = _httpClient.PostAsync("http://ip-api.com/batch", dataForPost).Result
            .Content.ReadAsStreamAsync().Result;
        var option = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        };
        var deserializeResponseBody = JsonSerializer.Deserialize<IList<CountryCodeContent>>(responseBodyFromGetCountryCode, option);
        _logger.LogInformation("The CountryCode I get is : " + deserializeResponseBody.FirstOrDefault().CountryCode );
        return deserializeResponseBody.FirstOrDefault().CountryCode ;
    }
}