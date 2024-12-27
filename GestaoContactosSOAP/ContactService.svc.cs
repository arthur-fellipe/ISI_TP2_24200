using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using GestaoContactosSOAP.Models;
using GestaoContactosSOAP;

namespace GestaoContactosSOAP
{
    public class ContactService : IContactService
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        // Retorna todos os contactos
        public List<Contact> GetAllContacts() => db.GetAllContacts();

        // Retorna um contacto pelo ID
        public Contact GetContactById(int id) => db.GetContactById(id);

        // Adiciona um novo contacto
        public int AddContact(Contact contact) => db.AddContact(contact);

        // Atualiza um contacto existente
        public int UpdateContact(Contact contact) => db.UpdateContact(contact);

        // Deleta um contacto pelo ID
        public int DeleteContact(int id) => db.DeleteContact(id);

        // Valida um utilizador com o nome de usuário e senha
        public bool ValidateUser(string username, string passwordHash) => db.ValidateUser(username, passwordHash);

        // Método para adicionar um novo usuário
        public void AddUser(string username, string passwordHash)
        {
            db.AddUser(username, passwordHash);  // Chama o método no DatabaseHelper
        }
    }
}
