using System.ComponentModel.DataAnnotations;

namespace Logic.Models.DocumentExportEnImport
{
    public class Leeruitkomst
    {
        [MaxLength(80)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        public Leerdoel? Leerdoel { get; set; }

        public List<Tentamen>? Tentamens { get; set; }

        public List<Les>? Lessen { get; set; }
    }
}
