using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClipMoney.Models.Request
{
    public class ChangeAccountAliasController : ApiController
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public string Alias { get; set; }
    }
}
