﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoContactosSOAP.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}