using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Tablas
{
    public class Usuarios
    {
        public Usuarios()
        {
        }

        public Usuarios(int id, string nombre, string apellido)
        {
            this.IdUsuario = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
        }

        public Usuarios(int id, string nombre, string apellido, string contraseña) 
        {
            this.IdUsuario = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Clave = contraseña;
        }

        public Usuarios(int IdCliente, string Cuil, string Nombre, string Apellido, string Contraseña, string Email, string Telefono, int IdSituacionCrediticia)
        {
            this.IdUsuario = IdCliente;
            this.Cuil = Cuil;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Clave = Contraseña;
            this.Email = Email;
            this.Telefono = Telefono;
            this.IdSituacionCrediticia = IdSituacionCrediticia;
        }


        public int? IdUsuario { get; set; }
        public string Cuil { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Clave { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int? IdSituacionCrediticia { get; set; }
        public string Privilegios { get; set; }
    }
}