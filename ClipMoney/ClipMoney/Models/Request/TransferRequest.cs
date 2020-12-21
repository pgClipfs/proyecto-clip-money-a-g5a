using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Request
{
    public class TransferRequest
    {
        public int DebitAccountId { get; set; }
        public decimal Amount { get; set; }
        public int CreditAccountId { get; set; }
        public string Concept { get; set; }
        public string DestinationReference { get; set; }
        public string EmailNotification { get; set; }
    }
}