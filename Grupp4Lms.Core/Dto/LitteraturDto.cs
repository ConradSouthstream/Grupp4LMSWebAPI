using System;

namespace Grupp4Lms.Core.Dto
{
    /// <summary>
    /// Dto för litteratur
    /// </summary>
    public class LitteraturDto
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
        /// Id för ämne
        /// </summary>
        public int AmneId { get; set; }

        /// <summary>
        /// Litteraturens nivå
        /// </summary>
        public string Niva { get; set; }

        /// <summary>
        /// Id för nivån
        /// </summary>
        public int NivaId { get; set; }
    }
}
