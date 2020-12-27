using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Request
{
    public class CreditCardDepositRequest
    {
        public string FullName { get; set; }
        public string  ExpirationDate { get; set; }
        public string CreditCardNumber { get; set; }
        public int SecurityNumber { get; set; }
        public int DocumentNumber { get; set; }
        public decimal Amount { get; set; }
        public int DebitAccountId { get; set; }

    }
}