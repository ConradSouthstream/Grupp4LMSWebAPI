using System;

namespace Grupp4Lms.Core.Dto
{
    public class ForfattareDto
    {
        public int ForfatterId { get; set; }
        public string ForNamn { get; set; }
        public string EfterNamn { get; set; }
        public DateTime FodelseDatum { get; set; }
    }
}
