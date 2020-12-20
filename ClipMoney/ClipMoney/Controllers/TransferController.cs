using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ClipMoney.Controllers
{
    [Authorize]
    public class TransferController : ApiController
    {

        public IHttpActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

                AccountManager accountManager = new AccountManager();
                List<Cuenta> accounts = accountManager.GetUserAccountsByUserId(Convert.ToInt32(UserId));

                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "Exito - cuentas del usuario obtenidas exitosamente";
                oRespuesta.Data = accounts;

                return Ok(oRespuesta);

            } catch(Exception ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Error - no se pudo obtener las cuentas del usuario";
                oRespuesta.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }
        }

    }
}
