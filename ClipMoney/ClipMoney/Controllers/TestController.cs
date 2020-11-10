using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ClipMoney.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        // get

        [ActionName("GetCuil")]
        [HttpPost]
        public IHttpActionResult Get([FromBody] Object request)
        {

            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

            string CUIL = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;

            return Ok("El cuil del usuario es: " + CUIL);
        }
    }
}
