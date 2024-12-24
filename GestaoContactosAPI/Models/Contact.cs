namespace GestaoContactosAPI.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int UserID { get; set; }
    }
}
