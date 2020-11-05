using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Request
{
    public class LoginRequest
    {
        public string Cuil { get; set; }
        public string Password { get; set; }
    }
}