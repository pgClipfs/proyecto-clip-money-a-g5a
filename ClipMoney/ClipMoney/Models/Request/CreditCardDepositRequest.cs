using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace ClipMoney.Models.Request
{
    public class CreditCardDepositRequest
    {
        [Required(ErrorMessage = "{0},No ingreso un nombre")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "{0},El nombre tiene que ser como minimo de 5 caracteres y menor que 30")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "{0},No ingreso una fecha de expiración")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "{0},La fecha de expiracion debe ser igual a 5")]
        public string  ExpirationDate { get; set; }

        [Required(ErrorMessage = "{0},No ingreso una tarjeta de crédito")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "{0},La longitud del numero de la tarjeta debe ser igual a 16")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "{0},No ingreso un número de seguridad")]
        [Range(100, 999, ErrorMessage = "{0},El numero de seguridad debe ser igual a 3")]
        public int SecurityNumber { get; set; }

        [Required(ErrorMessage = "{0},No ingreso un número de documento")]
        [Range(1000000, 100000000, ErrorMessage = "{0},Numero de documento incorrecto")]
        public int DocumentNumber { get; set; }

        [Required(ErrorMessage = "{0},No ingreso un monto a depositar")]
        [Range(200, 250000, ErrorMessage = "{0},El monto minimo a depositar es 200 y como maximo puede ser 50000")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "{0},No selecciono la cuenta a la que desea depositar")]
        [Range(0, int.MaxValue, ErrorMessage = "{0},El id de la cuenta a depositar tiene que ser positivo")]
        public int DebitAccountId { get; set; }

    }
}