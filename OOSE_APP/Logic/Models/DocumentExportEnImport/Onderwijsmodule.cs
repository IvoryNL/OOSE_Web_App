using Logic.DocumentExporter.Interfaces;
using Logic.DocumentImporter.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Logic.Models.DocumentExportEnImport
{
    public class Onderwijsmodule
    {
        [Required]
        public int OpleidingId { get; set; }

        public int StatusId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        [MaxLength(150)]
        public string Coordinator { get; set; }

        public int Studiepunten { get; set; }

        [MaxLength(30)]
        public string Fase { get; set; }

        [MaxLength(150)]
        public string Ingangseisen { get; set; }

        public int Leerjaar { get; set; }

        public decimal Versie { get; set; }

        public Opleiding? Opleiding { get; set; }

        public List<Onderwijseenheid>? Onderwijseenheden { get; set; }

        public List<Onderwijsuitvoering>? Onderwijsuitvoeringen { get; set; }

        [JsonIgnore]
        public IExportDocument<Onderwijsmodule> ExporteerDocument { get; set; }

        [JsonIgnore]
        public IImportDocument<Onderwijsmodule> ImporteerDocument { get; set; }
    }
}
