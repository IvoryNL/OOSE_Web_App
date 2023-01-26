using Logic.DocumentExporter.Interfaces;
using Logic.DocumentImporter.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Logic.Models.DocumentExportEnImport
{
    public class Onderwijseenheid
    {
        [MaxLength(50)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        [MaxLength(150)]
        public string Coordinator { get; set; }

        public decimal Studiepunten { get; set; }

        public List<Onderwijsmodule>? Onderwijsmodules { get; set; }

        public List<Leerdoel>? Leerdoelen { get; set; }

        [JsonIgnore]
        public IExportDocument<Onderwijseenheid> ExporteerDocument { get; set; }

        [JsonIgnore]
        public IImportDocument<Onderwijseenheid> ImporteerDocument { get; set; }
    }
}
