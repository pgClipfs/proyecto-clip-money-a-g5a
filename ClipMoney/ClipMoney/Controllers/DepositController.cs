using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Tablas;
using CreditCardValidator;
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
    public class DepositController : ApiController
    {
        [Route("api/Deposit/CreditCard")]
        [HttpGet]
        public IHttpActionResult Get(string number) {
            CreditCardDetector detector = new CreditCardDetector(number);
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var response = new
                {
                    IsValid = detector.IsValid(),
                    Brand = number.CreditCardBrandNameIgnoreLength(),
                };

                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "Exito - validacion de la tarjeta realizada";
                oRespuesta.Data = response;

                return Content(HttpStatusCode.OK, oRespuesta);

            } catch(Exception ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Error - no se pudo validar la tarjeta de credito";
                oRespuesta.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }


        }

        [Route("api/Deposit/CreditCard")]
        [HttpPost]
        public IHttpActionResult Post(CreditCardDepositRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            DepositManager oDepositManager = new DepositManager();
            AccountManager oAccountManager = new AccountManager();

            try
            {
                //var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                //string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;
                CreditCardDetector detector = new CreditCardDetector(model.CreditCardNumber);

                if (!detector.IsValid())
                {
                    throw new ArgumentException("Los datos de la tarjeta de credito no son valdios");
                }

                oDepositManager.DepositWithCreditCard(model.Amount, model.DebitAccountId, model.FullName, model.CreditCardNumber.Substring(model.CreditCardNumber.Length - 4), model.DocumentNumber, model.ExpirationDate);
                oAccountManager.UpdateAccountBalance(model.DebitAccountId, model.Amount);

                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "Exito - se ha realizado el deposito correctamente";

                return Content(HttpStatusCode.OK, oRespuesta);
            }
            catch (ArgumentException ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Error - no se ha podido realizar el deposito";
                oRespuesta.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }
            catch (Exception ex)
            {

                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Error - no se ha podido realizar el deposito";
                oRespuesta.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }
        }
    }
}
