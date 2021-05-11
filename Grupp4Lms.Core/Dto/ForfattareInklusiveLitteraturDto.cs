using System;
using System.Collections.Generic;

namespace Grupp4Lms.Core.Dto
{
    /// <summary>
    /// Dto för en författare inklusive den litteratus som hen har varit med och skrivit
    /// </summary>
    public class ForfattareInklusiveLitteraturDto
    {
        /// <summary>
        /// Författarens id
        /// </summary>
        public int ForfatterId { get; set; }

        /// <summary>
        /// Författarens förnamn
        /// </summary>
        public string ForNamn { get; set; }

        /// <summary>
        /// Författarens efternamn
        /// </summary>
        public string EfterNamn { get; set; }

        /// <summary>
        /// Författarens ålder
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Litteratur som författaren har varit med och skrivit
        /// </summary>
        public IEnumerable<LitteraturDto> Litteratur { get; set; }
    }
}
