namespace Logic.Models
{
    public class LoggedInUserModel : UserModel
    {

        public string JwtToken { get; set; }
        
        public Rol Rol { get; set; }
    }
}
