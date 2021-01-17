using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Tablas
{
    public class Account
    { 
        public int? AccountId { get; set; }
        public AccountType TypeAccount { get; set; }
        public Currency Currency { get; set; }
        public User User { get; set; }
        public string CVU { get; set; }
        public decimal? Balance { get; set; }
        public string Alias { get; set; }
        public DateTime OpeningDate { get; set; }
    }
}