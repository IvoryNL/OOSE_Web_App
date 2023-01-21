namespace Logic.Models.Dto
{
    public class IngelogdeGebruikerDto : BaseGebruikerModelDto
    {

        public Rol Rol { get; set; }
        public string JwtToken { get; set; }
    }
}
