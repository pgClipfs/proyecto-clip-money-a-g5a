using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClipMoney.Models.Request
{
    public class RegistrationRequest
    {
        public string Cuil { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<string> Images { get; set; }
    }
}