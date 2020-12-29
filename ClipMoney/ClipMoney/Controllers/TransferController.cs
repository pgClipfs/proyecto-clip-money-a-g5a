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
            GeneralResponse oResponse = new GeneralResponse();
            try
            {
                AccountManager oAccountManager = new AccountManager();

                Account oAccount = oAccountManager.GetAccountByCVU(cvu);

                var response = new BasicAccountResponse
                {
                    CVU = oAccount.CVU,
                    AccountId = oAccount.AccountId,
                    Owner = new
                    {
                        Name = oAccount.User.Name,
                        Surname = oAccount.User.Surname,
                        Cuil = oAccount.User.Cuil
                    }
                };

                oResponse.Success = 1;
                oResponse.Message = "Exito - cuenta obtenida";
                oResponse.Data = response;

                return Content(HttpStatusCode.OK, oResponse);
            } catch (Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - no se pudo obtener la cuenta";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] TransferRequest model)
        {
            GeneralResponse oResponse = new GeneralResponse();
            AccountManager oAccountManager = new AccountManager();
            TransferManager oTransferManager = new TransferManager();

            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

            string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;


            try
            {
                Account oAccount = new Account();

                oAccount = oAccountManager.GetAccountById(model.DebitAccountId);

                if(oAccount.AccountId == null)
                {
                    throw new ArgumentNullException("Cuenta de debito no encontrada");
                }

                oTransferManager.MakeTransfer(oAccount, model.CreditAccountId, model.Amount, model.Concept);
                oAccountManager.UpdateAccountBalance((int)oAccount.AccountId, -model.Amount);
                oAccountManager.UpdateAccountBalance(model.CreditAccountId, model.Amount);
                oResponse.Success = 1;
                oResponse.Message = "Exito - Transferencia realizada";

                return Content(HttpStatusCode.OK, oResponse);

            } catch(ArgumentNullException ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - " + ex.Message;
                return Content(HttpStatusCode.BadRequest, oResponse);
            } catch(Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = ex.Message;
                return Content(HttpStatusCode.BadRequest, oResponse);
            }

        }
    }
}
