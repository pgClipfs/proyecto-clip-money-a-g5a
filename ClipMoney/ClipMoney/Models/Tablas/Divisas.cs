using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Tablas
{
    public class Divisas
    {
        public int IdDivisa { get; set; }
        public string Divisa { get; set; }
        public double Fee { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}