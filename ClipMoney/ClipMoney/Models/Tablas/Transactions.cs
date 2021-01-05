using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Tablas
{
    public class Transactions
    {
        public string TransactionType { get; set; }
        public DateTime DateTime { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Concept { get; set; }
        public int VoucherNumber { get; set; }

    }
}