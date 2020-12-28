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
    public class UserController : ApiController
    {

        public IHttpActionResult Get()
        {
            Respuesta oResponse = new Respuesta();
            UsuarioGestor oUserManager = new UsuarioGestor();

            try
            {
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

                Usuarios oUser = oUserManager.GetByUserId(UserId);
                oUser.Clave = null;

                oResponse.Exito = 1;
                oResponse.Mensaje = "Exito - se obtuvo los datos del usuario";
                oResponse.Data = oUser;

                return Content(HttpStatusCode.OK, oResponse);
            } catch(Exception ex)
            {
                oResponse.Exito = 0;
                oResponse.Mensaje = "Error - no se pudo obtener los datos del usuario";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }

    }
}
