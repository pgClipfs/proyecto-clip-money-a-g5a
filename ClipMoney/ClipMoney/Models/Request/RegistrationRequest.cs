﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClipMoney.Models.Request
{
    public class RegistrationRequest
    {
        public string Cuil { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Images { get; set; }
    }
}