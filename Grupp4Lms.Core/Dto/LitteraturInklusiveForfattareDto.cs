using System;
using System.Collections.Generic;

namespace Grupp4Lms.Core.Dto
{
    public class LitteraturInklusiveForfattareDto
    {
        public int LitteraturId { get; set; }
        public string Titel { get; set; }
        public DateTime UtgivningsDatum { get; set; }
        public string Beskrivning { get; set; }
        public string Url { get; set; }
        public string Amne { get; set; }
        public string Niva { get; set; }
        public IEnumerable<ForfattareDto> Forfattare { get; set; }
    }
}
