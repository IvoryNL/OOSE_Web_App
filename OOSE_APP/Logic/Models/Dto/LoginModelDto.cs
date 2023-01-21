namespace Logic.Models.Dto
{
    public class LoginModelDto
    {
        public LoginModelDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
