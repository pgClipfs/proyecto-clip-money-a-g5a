using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Response;
using ClipMoney.Models.Tablas;
using ClipMoney.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ClipMoney.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

                if(oAccount.AccountId == null) { throw new ArgumentException("Cuenta no existente"); }

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
            } catch (ArgumentException ex)
            {
                oResponse.Success = 0;
                oResponse.Message = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
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
            FieldsService oFieldsService = new FieldsService();

            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

            string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

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


                Account oDebitAccount = new Account();

                oDebitAccount = oAccountManager.GetAccountById(model.DebitAccountId);

                if(oDebitAccount.AccountId == null){ throw new ArgumentException("Cuenta de debito no existente"); };
                if(Convert.ToString(oDebitAccount.User.UserId) != UserId) { throw new ArgumentException("No se puede transferir dinero desde una cuenta que no pertenece al usuario logeado"); };
                if(oDebitAccount.Balance < model.Amount) { throw new ArgumentException("No tiene el saldo suficiente para realizar esta transferencia"); };

                Account oCreditAccount = new Account();
                oCreditAccount = oAccountManager.GetAccountById(model.CreditAccountId);

                if(oCreditAccount.AccountId == null) { throw new ArgumentException("Cuenta de credito no existente"); };

                oTransferManager.MakeTransfer(oDebitAccount, model.CreditAccountId, model.Amount, model.Concept);
                oAccountManager.UpdateAccountBalance((int)oDebitAccount.AccountId, -model.Amount);
                oAccountManager.UpdateAccountBalance(model.CreditAccountId, model.Amount);
                oResponse.Success = 1;
                oResponse.Message = "Exito - Transferencia realizada";

                return Content(HttpStatusCode.OK, oResponse);

            } catch(ArgumentException ex)
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
