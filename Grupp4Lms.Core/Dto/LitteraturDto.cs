using System;

namespace Grupp4Lms.Core.Dto
{
    public class LitteraturDto
    {
        public int LitteraturId { get; set; }
        public string Titel { get; set; }
        public DateTime UtgivningsDatum { get; set; }
        public string Beskrivning { get; set; }
        public string Url { get; set; }
        public string Amne { get; set; }
        public string Niva { get; set; }
    }
}
