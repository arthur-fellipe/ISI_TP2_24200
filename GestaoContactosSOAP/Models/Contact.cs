using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace GestaoContactosSOAP.Models
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Telefone { get; set; }
        [DataMember]
        public int UserID { get; set; }
    }
}