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
    public class TransactionsController : ApiController
    {
        public IHttpActionResult Get()
        {
            Respuesta oResponse = new Respuesta();

            try
            {
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

                TransactionManager oTransactionManager = new TransactionManager();
                List<Transactions> oTransactions = oTransactionManager.GetTransactions(UserId);

                oResponse.Exito = 1;
                oResponse.Mensaje = "Exito - transactiones obtenidas correctamente";
                oResponse.Data = oTransactions;

                return Content(HttpStatusCode.OK, oResponse);

            } catch(Exception ex)
            {
                oResponse.Exito = 0;
                oResponse.Mensaje = "Error - no se pudo obtener las transacciones";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);                
            }
        }
    }
}
