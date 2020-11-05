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

using WebApiSegura.Controllers;

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
    }
}