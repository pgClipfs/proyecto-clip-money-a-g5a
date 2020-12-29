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
    public class AccountController : ApiController
    {
        public IHttpActionResult Get()
        {
            GeneralResponse oResponse = new GeneralResponse();
            try
            {
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

                AccountManager accountManager = new AccountManager();
                List<Account> accounts = accountManager.GetUserAccountsByUserId(Convert.ToInt32(UserId));

                oResponse.Success = 1;
                oResponse.Message = "Exito - cuentas del usuario obtenidas exitosamente";
                oResponse.Data = accounts;

                return Ok(oResponse);

            }
            catch (Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - no se pudo obtener las cuentas del usuario";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }
    }
}
