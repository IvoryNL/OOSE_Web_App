namespace Logic.Models
{
    public class TentamenVanStudent
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int TentamenId { get; set; }

        public string Bestand { get; set; }

        public DateTime Datum { get; set; }
    }
}
