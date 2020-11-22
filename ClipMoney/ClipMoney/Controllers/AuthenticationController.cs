using ClipMoney.Models;
using ClipMoney.Models.Request;
using ClipMoney.Models.Response;
using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;


namespace ClipMoney.Controllers
{
    public class AuthenticationController : ApiController
    {
        // GET: Authentication
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginRequest User)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                UsuarioGestor gestor = new UsuarioGestor();
                LoginRespuesta oLoginRespuesta = new LoginRespuesta(); 
                
                Usuario usuario = gestor.ObtenerPorCUILPassword(User.Cuil, User.Password);

                if(usuario.IdCliente != null)
                {
                    oLoginRespuesta.Token = TokenGenerator.GenerateTokenJwt(User.Cuil);

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Acesso concedido";
                    oRespuesta.Data = oLoginRespuesta;

                    return Ok(oRespuesta);
                } else
                {
                    oRespuesta.Exito = 0;
                    oRespuesta.Mensaje = "Acesso denegado, cuil o cuil incorrecto";
                    return Ok(oRespuesta);
                }


            } catch (Exception ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Error";

                return BadRequest();
            }

            
        }

        [Route("api/Authentication/Registration")]
        [HttpPost]
        public IHttpActionResult Registration([FromBody] RegistrationRequest model)
        {
            UsuarioGestor gestor = new UsuarioGestor();
            Respuesta oRespuesta = new Respuesta();

            try
            {
                gestor.ValidarDatosUsuario(model);

                Usuario usuario = gestor.BuscarPersonaPorCuil(model.Cuil);
                if(usuario.IdCliente != null)
                {
                    oRespuesta.Exito = 0;
                    oRespuesta.Mensaje = "El usuario ya se encuentra registrado";

                    return Content(HttpStatusCode.BadRequest, oRespuesta);
                } 

                gestor.RegistrarUsuario(model);
                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "Usuario registrado con exito";

                return Ok(oRespuesta);
            }
            catch(FormatException ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "No se pudo registrar al usuario";
                oRespuesta.Data = ex.Message;


                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }
            catch(Exception ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "No se pudo registrar al usuario";
                oRespuesta.Data = ex.Message;


                return Content(HttpStatusCode.BadRequest, oRespuesta);
            }

        }
    }
}