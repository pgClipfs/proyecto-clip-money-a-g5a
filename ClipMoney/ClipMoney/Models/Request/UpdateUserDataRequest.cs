﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Request
{
    public class UpdateUserDataRequest
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}