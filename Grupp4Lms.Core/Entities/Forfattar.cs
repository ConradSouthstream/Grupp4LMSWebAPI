using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Entities
{
    public class Forfattar
    {
        [Key]
        public int ForfatterId { get; set; }


        [Required(ErrorMessage = "Ange författerens förnamn")]
        public string ForNamn { get; set; }


        [Required(ErrorMessage = "Ange författerens efternamn")]
        public string EfterNamn { get; set; }


        [Required(ErrorMessage = "Ange författerens födelsedatum")]
        public DateTime FodelseDatum { get; set; }


        // Navigation properties
        /// <summary>
        /// En författare kan har skrivit flera olika litterära verk
        /// </summary>
        public ICollection<Litteratur> Skrivit { get; set; }
    }
}
