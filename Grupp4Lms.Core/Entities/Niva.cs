using System.ComponentModel.DataAnnotations;

namespace Grupp4Lms.Core.Entities
{
    public class Niva
    {
        [Key]
        public int NivaId { get; set; }


        [Required(ErrorMessage = "Ange namn på nivån")]
        public string Namn { get; set; }
    }
}
