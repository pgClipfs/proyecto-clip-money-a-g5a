using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Response
{
    public class BasicAccountResponse
    {
        public string CVU { get; set; }
        public int? AccountId { get; set; }
        public object Owner { get; set; }
    }
}