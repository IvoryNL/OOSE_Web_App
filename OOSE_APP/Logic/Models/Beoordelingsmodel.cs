using Logic.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class Beoordelingsmodel
    {
        public int Id { get; set; }

        public int TentamenId { get; set; }

        public int DocentId { get; set; }

        public int StatusId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Naam { get; set; }

        public Tentamen? Tentamen { get; set; }

        public VolledigeGebruikerModelDto? Docent { get; set; }

        public List<Beoordelingsonderdeel>? Beoordelingsonderdelen { get; set; }
    }
}
