using GestaoContactosSOAP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestaoContactosSOAP
{
    public class DatabaseHelper
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GestaoContactosDB"].ConnectionString;

        // Métodos de Contactos
        public List<Contact> GetAllContacts()
        {
            List<Contact> contacts = new List<Contact>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Contactos";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Nome = reader["Nome"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefone = reader["Telefone"].ToString()
                    });
                }
            }
            return contacts;
        }

        public Contact GetContactById(int id)
        {
            Contact contact = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Contactos WHERE ID = @ID";  // SQL para buscar um contacto pelo ID

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();  // Abrir a conexão
                SqlDataReader reader = cmd.ExecuteReader();  // Executar a consulta

                if (reader.Read())  // Se encontrar o contacto
                {
                    contact = new Contact
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Nome = reader["Nome"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefone = reader["Telefone"].ToString(),
                    };
                }
            }
            return contact;  // Retorna o contacto encontrado, ou null se não encontrar nenhum
        }

        public void AddContact(Contact contact)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Contactos (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Usando parâmetros para evitar SQL Injection
                cmd.Parameters.AddWithValue("@Nome", contact.Nome);
                cmd.Parameters.AddWithValue("@Email", contact.Email);
                cmd.Parameters.AddWithValue("@Telefone", contact.Telefone);

                conn.Open();  // Abrir a conexão
                cmd.ExecuteNonQuery();  // Executar o comando (sem retorno de dados)
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Contactos SET Nome = @Nome, Email = @Email, Telefone = @Telefone WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Usando parâmetros para evitar SQL Injection
                cmd.Parameters.AddWithValue("@ID", contact.ID);
                cmd.Parameters.AddWithValue("@Nome", contact.Nome);
                cmd.Parameters.AddWithValue("@Email", contact.Email);
                cmd.Parameters.AddWithValue("@Telefone", contact.Telefone);

                conn.Open();  // Abrir a conexão
                cmd.ExecuteNonQuery();  // Executar o comando (sem retorno de dados)
            }
        }

        public void DeleteContact(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Contactos WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Usando parâmetro para evitar SQL Injection
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();  // Abrir a conexão
                cmd.ExecuteNonQuery();  // Executar o comando (sem retorno de dados)
            }
        }

        // Métodos de Utilizadores
        public void AddUser(string username, string passwordHash)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Utilizadores (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool ValidateUser(string username, string passwordHash)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Utilizadores WHERE Username = @Username AND PasswordHash = @PasswordHash";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}
