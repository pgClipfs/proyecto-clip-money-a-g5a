using ClipMoney.Models;
using ClipMoney.Models.Gestores;
using ClipMoney.Models.Request;
using ClipMoney.Models.Response;
using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BC = BCrypt.Net.BCrypt;

namespace ClipMoney.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginRequest User)
        {
            GeneralResponse oResponse = new GeneralResponse();

            try
            {
                UserManager oUserManager = new UserManager();
                LoginResponse oLoginRespuesta = new LoginResponse();

                User user = oUserManager.GetByCuil(User.Cuil);
                
                if(user.UserId == null)
                {
                    throw new ArgumentException("Acesso denegado, cuil o cuil incorrecto");
                }

                if (!BC.Verify(User.Password, user.Password))
                {
                    oResponse.Success = 0;
                    oResponse.Message = "Contraseña incorrecta";

                    return Content(HttpStatusCode.BadRequest, oResponse);
                }


                oLoginRespuesta.Token = TokenGenerator.GenerateTokenJwt(user.Cuil, user.UserId);

                oResponse.Success = 1;
                oResponse.Message = "Acesso concedido";
                oResponse.Data = oLoginRespuesta;

                return Ok(oResponse);
            } catch (ArgumentException ex)
            {
                oResponse.Success = 0;
                oResponse.Message = ex.Message;
                return Ok(oResponse);
            } 
            catch (Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "Error desconocido, no se pudo logear";
                oResponse.Data = ex.Message;

                return BadRequest();
            }
        }

        [Route("api/Authentication/Registration")]
        [HttpPost]
        public async Task<IHttpActionResult> RegistrationAsync()
        {
            HttpRequest request = HttpContext.Current.Request;

            RegistrationRequest model = new RegistrationRequest()
            {
                Name = request.Params["Name"],
                Surname = request.Params["Surname"],
                Cuil = request.Params["Cuil"],
                Password = request.Params["Password"],
                Email = request.Params["Email"],
                PhoneNumber = request.Params["PhoneNumber"],
                Images = Services.ImagesService.StoreImage(request)
            };


            UserManager oUserManager = new UserManager();
            AccountManager oAccountManager = new AccountManager();
            GeneralResponse oResponse = new GeneralResponse();

            try
            {
                oUserManager.ValidateUserData(model);

                User user = oUserManager.GetByCuil(model.Cuil);
                if (user.UserId != null) 
                {
                    throw new ArgumentException("El usuario ya se encuentra registrado");
                }

                int UserId = oUserManager.RegisterUser(model);
                oAccountManager.CreateNewAccount(1, 1, UserId, 0, null);
                oResponse.Success = 1;
                oResponse.Message = "Usuario registrado con exito";

                return Ok(oResponse);
            } catch(ArgumentException ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "No se pudo registrar al usuario";
                oResponse.Data = ex.Message;

                return Content(HttpStatusCode.BadRequest, oResponse);
            }
            catch (FormatException ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "No se pudo registrar al usuario";
                oResponse.Data = ex.Message;


                return Content(HttpStatusCode.BadRequest, oResponse);
            }
            catch (Exception ex)
            {
                oResponse.Success = 0;
                oResponse.Message = "No se pudo registrar al usuario";
                oResponse.Data = ex.Message;


                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }
    }
}