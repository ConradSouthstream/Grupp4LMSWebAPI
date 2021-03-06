using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Entities
{
    /// <summary>
    /// Entitet för Niva
    /// </summary>
    public class Niva
    {
        /// <summary>
        /// Nivåns id
        /// </summary>
        [Key]
        public int NivaId { get; set; }

        /// <summary>
        /// Nivåns namn
        /// </summary>
        [Required(ErrorMessage = "Ange namn på nivån")]
        public string Namn { get; set; }
    }
}
