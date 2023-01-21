using Logic.Models;
using Logic.Models.Dto;

namespace Presentation.ViewModels
{
    public class GebruikerViewModel
    {
        public VolledigeGebruikerModelDto Gebruiker { get; set; }
        
        public List<Klas> Klassen { get; set; }

        public List<Opleiding> Opleidingen { get; set; }

        public List<Rol> Rollen { get; set; }

        public string GeselecteerdeRolId { get; set; }

        public string GeselecteerdeKlasId { get; set; }

        public string GeselecteerdeOpleidingId { get; set; }
    }
}
