namespace Logic.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public string Email { get; set; }

        public string Usercode { get; set; }

        public int RolId { get; set; }
    }
}
