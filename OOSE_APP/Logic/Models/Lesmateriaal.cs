using Logic.DocumentExporter.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Logic.Models
{
    public class Lesmateriaal
    {
        public int Id { get; set; }

        public int AuteurId { get; set; }

        public int LesmateriaaltypeId { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Omschrijving { get; set; }

        public bool Verplicht { get; set; }

        public Auteur? Auteur { get; set; }

        public LesmateriaalType? LesmateriaalType { get; set; }

        public List<Les>? Lessen { get; set; }

        public List<LesmateriaalInhoud>? LesmateriaalInhoud { get; set; }

        [JsonIgnore]
        private IExportDocument<Lesmateriaal> ExporteerDocument { get; set; }

        public void SetExportStrategy(IExportDocument<Lesmateriaal> exportDocumentStrategy)
        {
            ExporteerDocument = exportDocumentStrategy;
        }

        public byte[] Exporteer()
        {
            return ExporteerDocument.ExportToDocument(this);
        }
    }
}
