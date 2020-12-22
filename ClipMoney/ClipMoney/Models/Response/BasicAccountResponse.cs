using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Response
{
    public class BasicAccountResponse
    {
        public string CVU { get; set; }
        public int? IdCuenta { get; set; }
        public object Propietario { get; set; }
    }
}