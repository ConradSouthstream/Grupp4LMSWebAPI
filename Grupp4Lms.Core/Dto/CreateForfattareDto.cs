using System;
using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Dto
{
    /// <summary>
    /// Dto för att skapa en författare
    /// </summary>
    public class CreateForfattareDto
    {
        //public int ForfatterId { get; set; }

        /// <summary>
        /// Författarens Förnamn
        /// </summary>
        [Required(ErrorMessage = "Ange författerens förnamn")]
        public string ForNamn { get; set; }

        /// <summary>
        /// Författarens Efternamn
        /// </summary>
        [Required(ErrorMessage = "Ange författerens efternamn")]
        public string EfterNamn { get; set; }

        /// <summary>
        /// Författarens födelsedatum
        /// </summary>
        [Required(ErrorMessage = "Ange författerens födelsedatum")]
        public DateTime FodelseDatum { get; set; }

        /// <summary>
        /// Id för den litteratur som författaren skall kopplas till vid edit, create och delete
        /// Sätts av klienten
        /// </summary>
        public int LitteraturId { get; set; }
    }
}
