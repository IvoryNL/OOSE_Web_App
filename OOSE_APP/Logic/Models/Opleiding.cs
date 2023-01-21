using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Opleiding
    {
        public int Id { get; set; }

        public int VormId { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }
    }
}
