﻿using System.ComponentModel.DataAnnotations;

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
