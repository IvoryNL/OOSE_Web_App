using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class Leeruitkomst
    {
        public int Id { get; set; }

        public int LeerdoelId { get; set; }

        [MaxLength(80)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        public Leerdoel? Leerdoel { get; set; }

        public List<Tentamen>? Tentamens { get; set; }

        public List<Les>? Lessen { get; set; }
    }
}
