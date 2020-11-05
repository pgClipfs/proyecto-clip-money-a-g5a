using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Tablas
{
    public class Usuario
    {
        public Usuario()
        {
        }

        public Usuario(int id, string nombre, string apellido)
        {
            this.IdCliente = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
        }

        public Usuario(int IdCliente, string Cuil, string Nombre, string Apellido, string Contraseña, string Email, string Telefono, int IdSituacionCrediticia)
        {
            this.IdCliente = IdCliente;
            this.Cuil = Cuil;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Contraseña = Contraseña;
            this.Email = Email;
            this.Telefono = Telefono;
            this.IdSituacionCrediticia = IdSituacionCrediticia;
        }


        public int? IdCliente { get; set; }
        public string Cuil { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int IdSituacionCrediticia { get; set; }
    }
}