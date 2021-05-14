using System;

namespace Grupp4Lms.Core.Dto
{
    /// <summary>
    /// Dto objekt för författare
    /// </summary>
    public class ForfattareDto
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
    }
}
