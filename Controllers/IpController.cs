using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class IpController : ControllerBase
{
    private readonly IpService _ipService;
    
    public IpController(IpService ipService)
    {
        _ipService = ipService;
    }

    [HttpGet("GetIp")]
    public IpContent? GetIp()
    {
        return _ipService.GetIp();
    }

    [HttpGet("GetCountryCode")]
    public string GetCountryCode()
    {
        return _ipService.GetCountry();
    }

    
}