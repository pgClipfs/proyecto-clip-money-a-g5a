using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Tablas
{
    public class Cuenta
    { 
        public int? IdCuenta { get; set; }
        public TipoCuentas TipoCuenta { get; set; }
        public Divisas Divisa { get; set; }
        public Usuarios Usuario { get; set; }
        public string CVU { get; set; }
        public decimal Saldo { get; set; }
        public string Alias { get; set; }
        public DateTime OpeningDate { get; set; }
    }
}