using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Entities
{
    public class Amne
    {
        [Key]
        public int AmneId { get; set; }


        [Required(ErrorMessage = "Ange namn på ämnet")]
        public string Namn { get; set; }
    }
}