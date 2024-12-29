namespace GestaoContactosAPI.Models
{
    // Modelo de utilizador
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

    }
}
