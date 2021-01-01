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
using System.Web.Http.Cors;

namespace ClipMoney.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class TransactionsController : ApiController
    {
        public IHttpActionResult Get()
        {
            GeneralResponse oResponse = new GeneralResponse();

            try
            {
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

                TransactionManager oTransactionManager = new TransactionManager();
                List<Transactions> oTransactions = oTransactionManager.GetTransactions(UserId);

                oResponse.Success = 1;
                oResponse.Message = "Exito - transactiones obtenidas correctamente";
                oResponse.Data = oTransactions;

                return Content(HttpStatusCode.OK, oResponse);

            } catch(Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - no se pudo obtener las transacciones";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);                
            }
        }
    }
}
