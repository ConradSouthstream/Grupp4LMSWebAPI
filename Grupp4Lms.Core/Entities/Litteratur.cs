using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp4Lms.Core.Entities
{
    /// <summary>
    /// Entitet för Litteratur
    /// </summary>
    public class Litteratur
    {
        /// <summary>
        /// Litteraturens id
        /// </summary>
        [Key]
        public int LitteraturId { get; set; }

        /// <summary>
        /// Litteraturens titel
        /// </summary>
        [Required(ErrorMessage = "Ange titel")]
        public string Titel { get; set; }

        /// <summary>
        /// Litteraturens datum
        /// </summary>
        [Required(ErrorMessage = "Ange utgivningsdatum")]
        public DateTime UtgivningsDatum { get; set; }

        /// <summary>
        /// Beskrivning av litteraturen
        /// </summary>
        [Required(ErrorMessage = "Skriv in en beskrivning")]
        public string Beskrivning { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        // Navigation properties

        /// <summary>
        /// ForeignKey till litteraturen Ämne i tabellen Amne
        /// </summary>
        [ForeignKey("Amne")]
        public int AmneId { get; set; }

        /// <summary>
        /// Litteraturens ämne
        /// </summary>
        public Amne Amne { get; set; }

        /// <summary>
        /// ForeignKey till literaturens nivå i tabellen Niva
        /// </summary>
        [ForeignKey("Niva")]
        public int NivaId { get; set; }

        /// <summary>
        /// Litteraturens nivå
        /// </summary>
        public Niva Niva { get; set; }

        /// <summary>
        /// Litteratur kan ha flera författare
        /// </summary>
        public ICollection<Forfattare> Forfattare { get; set; }
    }
}
