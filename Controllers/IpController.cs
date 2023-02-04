using CSharpSKA.Services;
using Microsoft.AspNetCore.Mvc;
using Polly;

namespace CSharpSKA.Controllers;

[ApiController]
[Route("[controller]")]
public class IpController : ControllerBase
{
    private readonly IpService _ipService;
    private ILogger<IpController> _logger;

    public IpController(IpService ipService, ILogger<IpController> logger)
    {
        _ipService = ipService;
        _logger = logger;
    }

    [HttpGet("GetIp")]
    public IpContent? GetIp()
    {
        var retryPolicy = Policy.Handle<HttpRequestException>()
            .Retry(3, (exception, retryCount) => { _logger.LogError("Get IP Failed!"); });
        return retryPolicy.Execute(()=>_ipService.GetIp()); 
    }

    [HttpGet("GetCountryCode")]
    public string GetCountryCode()
    {
        return _ipService.GetCountry();
    }

    
}