using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Logic.Models
{
    public class Planning
    {
        public int Id { get; set; }

        public int OnderwijsuitvoeringId { get; set; }

        [DisplayName("Datum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Datum { get; set; } = DateTime.Now;

        public int Weeknummer { get; set; }

        public Onderwijsuitvoering? Onderwijsuitvoering { get; set; }

        public List<Tentamen>? Tentamens { get; set; }

        public List<Les>? Lessen { get; set; }

        public Planning(DateTime datum, int weeknummer, int onderwijsuitvoeringId)
        {
            Datum = datum;
            Weeknummer = weeknummer;
            OnderwijsuitvoeringId = onderwijsuitvoeringId;
        }
    }
}
