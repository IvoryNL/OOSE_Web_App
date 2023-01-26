using System.ComponentModel.DataAnnotations;

namespace Logic.Models.DocumentExportEnImport
{
    public class Leerdoel
    {
        [MaxLength(80)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        public Onderwijseenheid? Onderwijseenheid { get; set; }

        public List<Leeruitkomst>? Leeruitkomsten { get; set; }
    }
}
