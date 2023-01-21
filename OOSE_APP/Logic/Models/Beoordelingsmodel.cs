using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Beoordelingsmodel
    {
        public int Id { get; set; }

        public int TentamenId { get; set; }

        public int DocentId { get; set; }

        public int StatusId { get; set; }
    }
}
