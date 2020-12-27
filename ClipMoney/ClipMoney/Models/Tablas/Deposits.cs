using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Tablas
{
    public class Deposits
    {
        public int DepositId { get; set; }
        public DateTime DepositDateTime { get; set; }
        public string DepositType { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public string CreditCardFullName { get; set; }
        public string CreditCardNumber { get; set; }
        public int DocumentNumber { get; set; }
        public string ExpirationDate { get; set; }
    }
}