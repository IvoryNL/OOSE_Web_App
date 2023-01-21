using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class LesmateriaalType
    {
        public int Id { get; set; }

        [MaxLength(80)]
        public string Naam { get; set; }
    }
}
