using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Request
{
    public class WithdrawRequest
    {
        [Required(ErrorMessage = "{0},Es necesario especificar una cuenta")]
        [Range(1, Int32.MaxValue, ErrorMessage = "El id de la cuenta debe ser mayor a 0")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "{0},Es necesario especificar el monto a retirar")]
        [Range(250, Int32.MaxValue, ErrorMessage = "{0},El monto a retirar debe ser mayor a 250")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "{0},Es necesario especificar si el retiro es descubierto o no")]
        public bool Uncovered { get; set; }

    }
}