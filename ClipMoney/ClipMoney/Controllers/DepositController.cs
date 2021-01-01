using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Tablas;
using ClipMoney.Services;
using CreditCardValidator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Net.Http.Headers;
using FastReport;
using FastReport.Export;
using FastReport.Export.PdfSimple;
using System.Web.Http.Cors;

namespace ClipMoney.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class DepositController : ApiController
    {
        [Route("api/Deposit/Cash")]
        [HttpGet]
        public HttpResponseMessage GetPdf(string CVU)
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;


            try
            {
                AccountManager oAccountManager = new AccountManager();
                ICollection<Account> oAccounts = oAccountManager.GetUserAccountsByUserId(Convert.ToInt32(UserId));

                if(!oAccounts.Where(a => a.CVU == CVU).Any())
                {
                    throw new ArgumentException("El cvu especificado no pertenece al usuario logeado");
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    var oReport = new Report();
                    oReport.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/Boleta.frx") );
                    oReport.SetParameterValue("pCVU", CVU);
                    oReport.SetParameterValue("pCODIGO", "ClipMoney-" + UserId + "-" + CVU);

                    oReport.Prepare();

                    PDFSimpleExport oExport = new PDFSimpleExport();

                    oExport.Export(oReport, ms);
                    ms.Flush();


                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(ms.ToArray());
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "Factura.pdf";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                    return response;
                }
            } catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);

                return response;
            }


        }

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
            FieldsService oFieldsService = new FieldsService();

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
