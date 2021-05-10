using System;
using System.Collections.Generic;

namespace Grupp4Lms.Core.Dto
{
    public class ForfattareInklusiveLitteraturDto
    {
        public int ForfatterId { get; set; }
        public string ForNamn { get; set; }
        public string EfterNamn { get; set; }
        public DateTime FodelseDatum { get; set; }
        public IEnumerable<LitteraturDto> Litteratur { get; set; }
    }
}
