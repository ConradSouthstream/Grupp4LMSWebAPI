using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp4Lms.Core.Entities
{
    public class Litteratur
    {
        [Key]
        public int LitteraturId { get; set; }


        [Required(ErrorMessage = "Ange utgivningsdatum")]
        public DateTime UtgivningsDatum { get; set; }


        [Required(ErrorMessage = "Skriv in en beskrivning")]
        public string Beskrivning { get; set; }


        // Navigation properties

        [ForeignKey("Amne")]
        public int AmneId { get; set; }

        public Amne Amne { get; set; }

        [ForeignKey("Niva")]
        public int NivaId { get; set; }

        public Niva Niva { get; set; }

        /// <summary>
        /// Litteratur kan ha flera författare
        /// </summary>
        public ICollection<Forfattar> Forfattare { get; set; }
    }
}
