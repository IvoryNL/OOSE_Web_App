using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Logic.Models
{
    public class Leerdoel
    {
        public int Id { get; set; }

        public int OnderwijseenheidId { get; set; }

        [MaxLength(80)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        public Onderwijseenheid? Onderwijseenheid { get; set; }

        public List<Leeruitkomst>? Leeruitkomsten { get; set; }
    }
}
