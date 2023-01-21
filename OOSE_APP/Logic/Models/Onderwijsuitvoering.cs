namespace Logic.Models
{
    public class Onderwijsuitvoering
    {
        public int Id { get; set; }

        public int OnderwijsmoduleId { get; set; }

        public int DocentId { get; set; }

        public int Jaartal { get; set; }

        public int Periode { get; set; }

        public Onderwijsmodule? Onderwijsmodule { get; set; }

        public List<Klas>? Klassen { get; set; }

        public List<Planning>? Planningen { get; set; }
    }
}