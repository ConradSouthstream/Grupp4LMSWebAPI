using System;
using System.Collections.Generic;

namespace Grupp4Lms.Core.Dto
{
    /// <summary>
    /// Dto för litteratur inklusive författare
    /// </summary>
    public class LitteraturInklusiveForfattareDto
    {
        /// <summary>
        /// Litteraturens id
        /// </summary>
        public int LitteraturId { get; set; }

        /// <summary>
        /// Litteraturens titel
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// Litteraturens utgivningsdatum
        /// </summary>
        public DateTime UtgivningsDatum { get; set; }

        /// <summary>
        /// Beskrivning av Litteraturen
        /// </summary>
        public string Beskrivning { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Litteraturens ämens kategori
        /// </summary>
        public string Amne { get; set; }

        /// <summary>
        /// Litteraturens nivå
        /// </summary>
        public string Niva { get; set; }

        public int AmneId { get; set; }

        public int NivaId { get; set; }

        /// <summary>
        /// Litteraturens författare
        /// </summary>
        public IEnumerable<ForfattareDto> Forfattare { get; set; }
    }
}
