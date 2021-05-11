using System;
using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Dto
{
    /// <summary>
    /// Dto för att redigera forfattaren
    /// </summary>
    public class EditForfattareDto
    {
        /// <summary>
        /// Författarens primärnyckel
        /// </summary>
        public int ForfatterId { get; set; }

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
    }
}
