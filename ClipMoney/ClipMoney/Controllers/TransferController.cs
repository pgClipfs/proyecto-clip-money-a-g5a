using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Response;
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

        public IHttpActionResult GetAccount(string cvu)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                AccountManager oAccountManager = new AccountManager();

                Cuenta oAccount = oAccountManager.GetAccountByCVU(cvu);

                var response = new BasicAccountResponse
                {
                    CVU = oAccount.CVU,
                    IdCuenta = oAccount.IdCuenta,
                    Propietario = new
                    {
                        Nombre = oAccount.Usuario.Nombre,
                        Apellido = oAccount.Usuario.Apellido,
                        CUIL = oAccount.Usuario.Cuil
                    }
                };

                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "Exito - cuenta obtenida";
                oRespuesta.Data = response;

                return Content(HttpStatusCode.OK, oRespuesta);
            } catch (Exception ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Error - no se pudo obtener la cuenta";
                oRespuesta.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] TransferRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            AccountManager accountManager = new AccountManager();
            TransferManager transferManager = new TransferManager();

            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

            string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;


            try
            {
                Cuenta oAccount = new Cuenta();

                oAccount = accountManager.GetAccountById(model.DebitAccountId);

                if(oAccount.IdCuenta == null)
                {
                    throw new ArgumentNullException("Cuenta de debito no encontrada");
                }

                transferManager.MakeTransfer(oAccount, model.CreditAccountId, model.Amount, model.Concept);
                accountManager.UpdateAccountBalance((int)oAccount.IdCuenta, -model.Amount);
                accountManager.UpdateAccountBalance(model.CreditAccountId, model.Amount);
                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "Exito - Transferencia realizada";

                return Content(HttpStatusCode.OK, oRespuesta);

            } catch(ArgumentNullException ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Error - " + ex.Message;
                return Content(HttpStatusCode.BadRequest, oRespuesta);
            } catch(Exception ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = ex.Message;
                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }

        }
    }
}
