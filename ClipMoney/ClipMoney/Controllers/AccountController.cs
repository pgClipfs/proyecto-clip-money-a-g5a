using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Tablas;
using ClipMoney.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ClipMoney.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

        [Route("api/Account/Alias")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] ChangeAccountAliasController model)
        {
            GeneralResponse oResponse = new GeneralResponse();
            FieldsService oFieldsService = new FieldsService();
            AccountManager oAccountManager = new AccountManager();

            try
            {
                Dictionary<string, string> oErrors = oFieldsService.ValidateModel(ModelState);
                if (oErrors.Count != 0)
                {
                    oResponse.Success = 0;
                    oResponse.Message = "Error, campos invalidos";
                    oResponse.Data = oErrors;

                    return Content(HttpStatusCode.BadRequest, oResponse);
                }

                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;
                List<Account> oAccounts = oAccountManager.GetUserAccountsByUserId(Convert.ToInt32(UserId));

                if (!oAccounts.Where(m => m.AccountId == model.AccountId).Select(c => c).Any())
                {
                    throw new ArgumentException("La cuenta a la que desea depositar no pertenece al usuario logeado");
                };

                oAccountManager.SetAccountAlias(model.Alias, model.AccountId);

                oResponse.Success = 1;
                oResponse.Message = "Se ha establecido correctamente el nuevo alias";

                return Content(HttpStatusCode.OK, oResponse);
            }
            catch (Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "No se ha podido establecer el nuevo alias, error: " + ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }

    }
}
