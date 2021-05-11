using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Dto
{
    /// <summary>
    /// Dto för att skapa litteratur
    /// </summary>
    public class CreateLitteraturDto
    {
        //public int LitteraturId { get; set; }

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

        /// <summary>
        /// ForeignKey till litteraturen Ämne i tabellen Amne
        /// </summary>
        [Required(ErrorMessage = "Ni måste ange ämne")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Ni måste ange ämne")]
        public int AmneId { get; set; }

        /// <summary>
        /// ForeignKey till literaturens nivå i tabellen Niva
        /// </summary>
        [Required(ErrorMessage = "Ni måste ange nivå")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Ni måste ange nivå")]
        public int NivaId { get; set; }

        /// <summary>
        /// Litteratur kan ha flera författare
        /// </summary>
        public ICollection<CreateForfattareDto> Forfattare { get; set; }
    }
}
