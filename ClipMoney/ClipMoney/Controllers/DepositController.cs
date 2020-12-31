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
using System.Web.Http.ModelBinding;

namespace ClipMoney.Controllers
{
    [Authorize]
    public class DepositController : ApiController
    {
        [Route("api/Deposit/CreditCard")]
        [HttpGet]
        public IHttpActionResult Get(string number) {
            GeneralResponse oResponse = new GeneralResponse();

            try
            {
                CreditCardDetector detector = new CreditCardDetector(number);
                var response = new
                {
                    IsValid = detector.IsValid(),
                    Brand = number.CreditCardBrandNameIgnoreLength(),
                };

                oResponse.Success = 1;
                oResponse.Message = "Exito - validacion de la tarjeta realizada";
                oResponse.Data = response;

                return Content(HttpStatusCode.OK, oResponse);

            } catch(Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - no se pudo validar la tarjeta de credito";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }


        }

        [Route("api/Deposit/CreditCard")]
        [HttpPost]
        public IHttpActionResult Post(CreditCardDepositRequest model)
        {
            GeneralResponse oResponse = new GeneralResponse();
            DepositManager oDepositManager = new DepositManager();
            AccountManager oAccountManager = new AccountManager();

            try
            {
                if (!ModelState.IsValid)
                {
                    var oErrorsDictionary = new Dictionary<string, string>();

                    foreach(var state in ModelState.Values)
                    {
                        foreach(var error in state.Errors)
                        {
                            var aErrors = error.ErrorMessage.Split(',');
                            oErrorsDictionary.Add(aErrors[0], aErrors[1]);
                        }
                    }


                    oResponse.Success = 0;
                    oResponse.Message = "Error, campos invalidos";
                    oResponse.Data = oErrorsDictionary;

                    return Content(HttpStatusCode.BadRequest, oResponse);
                }

                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;
                List<Account> oAccounts = oAccountManager.GetUserAccountsByUserId(Convert.ToInt32(UserId));

                if (!oAccounts.Where(m => m.AccountId == model.DebitAccountId).Select(c => c).Any()) {
                    throw new ArgumentException("La cuenta a la que desea depositar no pertenece al usuario logeado");
                };

                CreditCardDetector detector = new CreditCardDetector(model.CreditCardNumber);

                if (!detector.IsValid())
                {
                    throw new ArgumentException("Los datos de la tarjeta de credito no son valdios");
                }

                oDepositManager.DepositWithCreditCard(model.Amount, model.DebitAccountId, model.FullName, model.CreditCardNumber.Substring(model.CreditCardNumber.Length - 4), model.DocumentNumber, model.ExpirationDate);
                oAccountManager.UpdateAccountBalance(model.DebitAccountId, model.Amount);

                oResponse.Success = 1;
                oResponse.Message = "Exito - se ha realizado el deposito correctamente";

                return Content(HttpStatusCode.OK, oResponse);
            }
            catch (ArgumentException ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - no se ha podido realizar el deposito";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
            catch (Exception ex)
            {

                oResponse.Success = 0;
                oResponse.Message = "Error - no se ha podido realizar el deposito";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }
    }
}
