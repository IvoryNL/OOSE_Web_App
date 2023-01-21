using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class Vorm
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }
    }
}
