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
        private IExportDocument<Onderwijsmodule> ExporteerDocument { get; set; }

        [JsonIgnore]
        private IImportDocument<Onderwijsmodule> ImporteerDocument { get; set; }

        public void SetExportStrategy(IExportDocument<Onderwijsmodule> exportDocumentStrategy)
        {
            ExporteerDocument = exportDocumentStrategy;
        }

        public byte[] Exporteer()
        {
            return ExporteerDocument.ExportToDocument(this);
        }

        public void SetImportStrategy(IImportDocument<Onderwijsmodule> importDocumentStrategy)
        {
            ImporteerDocument = importDocumentStrategy;
        }

        public Onderwijsmodule Importeer(byte[] contentBytes)
        {
            return ImporteerDocument.ImportDocument(contentBytes);
        }
    }
}
