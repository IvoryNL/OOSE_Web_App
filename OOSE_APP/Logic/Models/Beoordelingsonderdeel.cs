using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class Beoordelingsonderdeel
    {
        public int Id { get; set; }

        public int BeoordelingsmodelId { get; set; }

        [MaxLength(80)]
        public string Titel { get; set; }

        public decimal Resultaat { get; set; }

        public decimal Gewicht { get; set; }

        public decimal Grens { get; set; }

        public bool Verplicht { get; set; }

        public Beoordelingsmodel? Beoordelingsmodel { get; set; }

        public List<Beoordelingscriteria>? Beoordelingscriterium { get; set; }
    }
}
