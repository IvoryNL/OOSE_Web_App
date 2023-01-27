namespace Logic.Models
{
    public class Beoordeling
    {
        public int Id { get; set; }

        public int DocentId { get; set; }

        public int BeoordelingsmodelId { get; set; }

        public int? TentamenUploadId { get; set; }

        public DateTime Datum { get; set; }

        public decimal Resultaat { get; set; }

        public Beoordelingsmodel? Beoordelingsmodel { get; set; }

        public TentamenVanStudent? TentamenVanStudent { get; set; }
    }
}
