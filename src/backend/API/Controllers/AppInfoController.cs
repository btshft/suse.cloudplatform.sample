using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Suse.CloudPlatform.API.Controllers
{
    [ApiController, Route("app/info")]
    public class AppInfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var connection = HttpContext.Features.Get<IHttpConnectionFeature>();
            if (connection == null)
            {
                return NoContent();
            }

            var serverIp = connection.LocalIpAddress != null
                ? connection.LocalIpAddress.IsIPv4MappedToIPv6
                    ? connection.LocalIpAddress.MapToIPv4()
                    : connection.LocalIpAddress
                : null;

            return Ok(new
            {
                ServerIp = serverIp?.ToString()
            });
        }
    }
}
