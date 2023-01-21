using System.ComponentModel.DataAnnotations;

namespace Logic.Models.Dto
{
    public class CreateGebruikerModelDto : BaseGebruikerModelDto
    {
        [Required]
        public string Password { get; set; }
    }
}
