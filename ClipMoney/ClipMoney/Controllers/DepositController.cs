using ClipMoney.Models;
using CreditCardValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClipMoney.Controllers
{
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
    }
}
