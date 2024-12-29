using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace GestaoContactosSOAP.Models
{
    // Modelo de Contacto
    [DataContract] // Define a classe como um contrato de dados
    public class Contact
    {
        [DataMember] // Define a propriedade como um membro do contrato de dados
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