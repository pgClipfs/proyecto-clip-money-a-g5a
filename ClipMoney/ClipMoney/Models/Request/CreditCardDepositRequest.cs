using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace ClipMoney.Models.Request
{
    public class CreditCardDepositRequest
    {
        [Required(ErrorMessage = "FullName,No ingreso un nombre")]
        [StringLength(30, MinimumLength = 5, ErrorMessage ="FullName,El nombre tiene que ser como minimo de 5 caracteres y menor que 30")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "ExpirationDate,No ingreso una fecha de expiración")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "ExpirationDate,La fecha de expiracion debe ser igual a 5")]
        public string  ExpirationDate { get; set; }

        [Required(ErrorMessage = "CreditCardNumber,No ingreso una tarjeta de crédito")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "CreditCardNumber,La longitud del numero de la tarjeta debe ser igual a 16")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "SecurityNumber,No ingreso un número de seguridad")]
        [Range(100, 999, ErrorMessage = "SecurityNumber,El numero de seguridad debe ser igual a 3")]
        public int SecurityNumber { get; set; }

        [Required(ErrorMessage = "DocumentNumber,No ingreso un número de documento")]
        [Range(1000000, 100000000, ErrorMessage = "DocumentNumber,Numero de documento incorrecto")]
        public int DocumentNumber { get; set; }

        [Required(ErrorMessage = "Amount,No ingreso un monto a depositar")]
        [Range(200, 250000, ErrorMessage = "Amount,El monto minimo a depositar es 200 y como maximo puede ser 50000")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "DebitAccountId,No selecciono la cuenta a la que desea depositar")]
        [Range(0, int.MaxValue, ErrorMessage = "DebitAccountId,El id de la cuenta a depositar tiene que ser positivo")]
        public int DebitAccountId { get; set; }

    }
}