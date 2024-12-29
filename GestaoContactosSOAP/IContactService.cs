using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using GestaoContactosSOAP.Models;


namespace GestaoContactosSOAP
{
    [ServiceContract] // Define a interface como um serviço SOAP
    public interface IContactService
    {
        [OperationContract] // Define um método que pode ser chamado via SOAP
        List<Contact> GetAllContacts();

        [OperationContract]
        Contact GetContactById(int id);

        [OperationContract]
        int AddContact(Contact contact);

        [OperationContract]
        int UpdateContact(Contact contact);

        [OperationContract]
        int DeleteContact(int id);
    }
}
