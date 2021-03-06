using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Entities
{
    /// <summary>
    /// Entititet för Ämne
    /// </summary>
    public class Amne
    {
        /// <summary>
        /// Amnets id
        /// </summary>
        [Key]
        public int AmneId { get; set; }

        /// <summary>
        /// Amnets namn
        /// </summary>
        [Required(ErrorMessage = "Ange namn på ämnet")]
        public string Namn { get; set; }
    }
}