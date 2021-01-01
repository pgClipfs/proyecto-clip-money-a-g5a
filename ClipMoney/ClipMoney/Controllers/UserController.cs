using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Tablas;
using ClipMoney.Services;
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
    public class UserController : ApiController
    {

        public IHttpActionResult Get()
        {
            GeneralResponse oResponse = new GeneralResponse();
            UserManager oUserManager = new UserManager();

            try
            {
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

                User oUser = oUserManager.GetByUserId(UserId);

                var oUserResponse = new
                {
                    Cuil = oUser.Cuil,
                    Name = oUser.Name,
                    Surname = oUser.Surname,
                    Email = oUser.Email,
                    PhoneNumber = oUser.PhoneNumber,
                };

                oResponse.Success = 1;
                oResponse.Message = "Exito - se obtuvo los datos del usuario";
                oResponse.Data = oUserResponse;

                return Content(HttpStatusCode.OK, oResponse);
            } catch(Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - no se pudo obtener los datos del usuario";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeUserData(UpdateUserDataRequest model)
        {
            GeneralResponse oResponse = new GeneralResponse();
            FieldsService oFieldService = new FieldsService();
            Dictionary<string, string> oErrors = oFieldService.ValidateModel(ModelState);

            try
            {
                if (oErrors.Count != 0)
                {
                    throw new ArgumentException("Error, campos invalidos");
                }


                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                string UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value;

                UserManager oUserManager = new UserManager();
                oUserManager.UpdateDataByUserId(UserId, model.PhoneNumber, model.Email);

                User oUser = oUserManager.GetByUserId(UserId);

                var oUserResponse = new
                {
                    Cuil = oUser.Cuil,
                    Name = oUser.Name,
                    Surname = oUser.Surname,
                    Email = oUser.Email,
                    PhoneNumber = oUser.PhoneNumber,
                };

                oResponse.Success = 1;
                oResponse.Message = "Exito - se han registrado los nuevos datos del usuario";
                oResponse.Data = oUserResponse;

                return Content(HttpStatusCode.OK, oResponse);

            } catch(ArgumentException ex)
            {
                oResponse.Success = 0;
                oResponse.Message = ex.Message;
                oResponse.Data = oErrors;

                return Content(HttpStatusCode.BadRequest, oResponse);
            } 
            catch(Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error - no se ha podido registrar los nuevos datos del usuario";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }
    }
}
