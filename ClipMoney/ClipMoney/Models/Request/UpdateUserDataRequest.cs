using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Request
{
    public class UpdateUserDataRequest
    {
        [Required(ErrorMessage = "{0},Es necesario un numero de telefono")]
        [Range(1000000000, 9999999999, ErrorMessage = "{0},Numero de telefono incorrecto")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0},Por favor ingrese un email")]
        [EmailAddress(ErrorMessage = "{0},El email ingresado no es valido")]
        public string Email { get; set; }
    }
}