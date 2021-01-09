using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Request
{
    public class TransferRequest
    {
        [Required(ErrorMessage = "{0},Es necesario especificar una cuenta de debito")]
        [Range(1, Int64.MaxValue, ErrorMessage = "El id de la cuenta de destino debe ser mayor a 0")]
        public int DebitAccountId { get; set; }

        [Required(ErrorMessage = "{0},Es necesario especificar un monto a transferir")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "{0},Es necesario especificar una cuenta de credito")]
        [Range(1, Int64.MaxValue, ErrorMessage = "El id de la cuenta de destino debe ser mayor a 0")]
        public int CreditAccountId { get; set; }

        [Required(ErrorMessage = "{0},Es necesario especificar un concepto")]
        public string Concept { get; set; }

        public string DestinationReference { get; set; }
        public string EmailNotification { get; set; }
    }
}