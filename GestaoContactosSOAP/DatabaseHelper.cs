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
            try
            {
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
                            Telefone = reader["Telefone"].ToString(),
                            UserID = Convert.ToInt32(reader["UserID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                contacts = new List<Contact>();
            }
            return contacts;
        }


        public Contact GetContactById(int id)
        {
            Contact contact = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Contactos WHERE ID = @ID"; // SQL para buscar um contacto pelo ID

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        conn.Open(); // Abrir a conexão

                        using (SqlDataReader reader = cmd.ExecuteReader()) // Executar a consulta
                        {
                            if (reader.Read()) // Se encontrar o contacto
                            {
                                contact = new Contact
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Nome = reader["Nome"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefone = reader["Telefone"].ToString(),
                                    UserID = Convert.ToInt32(reader["UserID"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }

            return contact; // Retorna o contacto encontrado ou null caso haja erro
        }

            public int AddContact(Contact contact)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Contactos (Nome, Email, Telefone, UserID) VALUES (@Nome, @Email, @Telefone, @UserID); SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Usando parâmetros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@Nome", contact.Nome);
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                    cmd.Parameters.AddWithValue("@Telefone", contact.Telefone);
                    cmd.Parameters.AddWithValue("@UserID", contact.UserID);

                    conn.Open();  // Abrir a conexão
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());  // Executar o comando e obter o ID gerado
                    return newId > 0 ? 1 : 0;  // Retornar 1 se o ID for maior que 0, caso contrário 0
                }
            }
            catch (Exception)
            {
                // Em caso de erro, retornar 0
                return 0;
            }
        }

        public int UpdateContact(Contact contact)
        {
            try
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
                    int rowsAffected = cmd.ExecuteNonQuery();  // Executar o comando e obter o número de linhas afetadas
                    return rowsAffected;  // Retornar o número de linhas afetadas
                }
            }
            catch (Exception)
            {
                // Em caso de erro, retornar 0
                return 0;
            }
        }

        public int DeleteContact(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Contactos WHERE ID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Usando parâmetro para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();  // Abrir a conexão
                    int rowsAffected = cmd.ExecuteNonQuery();  // Executar o comando e obter o número de linhas afetadas
                    return rowsAffected;  // Retornar o número de linhas afetadas
                }
            }
            catch (Exception)
            {
                // Em caso de erro, retornar 0
                return 0;
            }
        }
    }
}
