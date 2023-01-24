using Logic.Models;
using Logic.Models.Dto;

namespace Presentation.ViewModels.Tentamens
{
    public class TentamenOverzichtViewModel
    {
        public VolledigeGebruikerModelDto? Gebruiker { get; set; }

        public List<Tentamen>? Tentamens { get; set; }
    }
}
