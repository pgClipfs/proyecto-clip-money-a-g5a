using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models
{
    public class GeneralResponse
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}