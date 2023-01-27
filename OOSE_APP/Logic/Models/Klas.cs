using System.ComponentModel.DataAnnotations;
using Logic.Models.Dto;

namespace Logic.Models
{
    public class Klas
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string Klasnaam { get; set; }

        public List<VolledigeGebruikerModelDto>? Gebruikers { get; set; }
    }
}
