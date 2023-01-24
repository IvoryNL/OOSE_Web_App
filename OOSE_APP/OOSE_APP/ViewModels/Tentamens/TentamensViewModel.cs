using Logic.Models;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Tentamens
{
    public class TentamensViewModel
    {
        public int TentamenId { get; set; }

        public string GeselecteerdeLeeruitkomstId { get; set; }

        public string GeselecteerdeOpleidingId { get; set; }

        public string GeselecteerdeOnderwijsuitvoeringId { get; set; }

        [Required]
        public DateTime Datum { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 52)]
        public int Weeknummer { get; set; } = 1;

        public List<Leeruitkomst>? Leeruitkomsten { get; set; }

        public List<Opleiding?> Opleidingen { get; set; }

        public List<Onderwijsuitvoering?> Onderwijsuitvoeringen { get; set; }
    }
}
