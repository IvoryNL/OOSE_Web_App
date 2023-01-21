using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class Auteur
    {
        public int Id { get; set; }

        [MaxLength(80)]
        public string Naam { get; set; }
    }
}
