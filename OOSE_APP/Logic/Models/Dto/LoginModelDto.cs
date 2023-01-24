using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Logic.Models.Dto
{
    public class LoginModelDto
    {
        public LoginModelDto()
        {

        }

        [DisplayName("E-mailadres")]
        [Required(ErrorMessage = "Het e-mailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Het e-mailadres is niet geldig")]
        public string Email { get; set; }

        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "Het wachtwoord is verplicht")]
        public string Wachtwoord { get; set; }
    }
}
