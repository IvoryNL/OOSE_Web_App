using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Opleidingsprofiel
    {
        public int Id { get; set; }

        public int OpleidingId { get; set; }

        [MaxLength(50)]
        public string Profielnaam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }
    }
}
