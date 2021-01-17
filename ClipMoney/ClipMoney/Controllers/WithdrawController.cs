using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Tablas;
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
    public class WithdrawController : ApiController
    {
        [HttpPost]
        public IHttpActionResult WithdrawMoney(WithdrawRequest model)
        {
            GeneralResponse oGeneralResponse = new GeneralResponse();

            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;
            AccountManager oAccountManager = new AccountManager();

            try
            {
                Account oUserAccount = oAccountManager.GetAccountById(model.AccountId);

                if (oUserAccount.AccountId == null) { throw new ArgumentException("El id de cuenta especificado no existe"); }
                if (Convert.ToString(oUserAccount.User.UserId) != UserId) { throw new ArgumentException("La cuenta especificada no pertenece al usuario logeado"); }

                if(model.Uncovered && model.Amount > oUserAccount.Balance * 1.1m) { throw new ArgumentException("No se puede tener un saldo descubierto mayor al 10% del saldo actual"); }
                if (!model.Uncovered && model.Amount > oUserAccount.Balance) { throw new ArgumentException("No tiene saldo suficiente para realizar esta transaccion"); }

                oAccountManager.UpdateAccountBalance(model.AccountId, -model.Amount);

                oGeneralResponse.Success = 1;
                oGeneralResponse.Message = "Exito - se ha realizado el retiro exitosamente";

                return Content(HttpStatusCode.OK, oGeneralResponse);
            } catch (ArgumentException ex)
            {
                oGeneralResponse.Success = 0;
                oGeneralResponse.Message = "Error - " + ex.Message;

                return Content(HttpStatusCode.BadRequest, oGeneralResponse);
            } catch (Exception ex)
            {
                oGeneralResponse.Success = 0;
                oGeneralResponse.Message = "Error - " + ex.Message;

                return Content(HttpStatusCode.BadRequest, oGeneralResponse);
            }
        }
    }
}
