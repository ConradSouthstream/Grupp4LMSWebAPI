using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Entities
{
    /// <summary>
    /// Entititet för författaren
    /// </summary>
    public class Forfattare
    {
        /// <summary>
        /// Författarens id
        /// </summary>
        [Key]
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


        // Navigation properties
        /// <summary>
        /// En författare kan har skrivit flera olika litterära verk
        /// </summary>
        public ICollection<Litteratur> Litteratur { get; set; }        
    }
}
