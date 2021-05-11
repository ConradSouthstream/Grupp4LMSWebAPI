using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupp4Lms.Core.IRepositories.Utils
{
    /// <summary>
    /// Klass med olika hjälp metoder för författare
    /// </summary>
    public class ForfattareHelper
    {
        /// <summary>
        /// Static metod som beräknar ålder från ett födelsedatum
        /// </summary>
        /// <param name="dtDateOfBirth">Födelsedatum</param>
        /// <returns>Ålder</returns>
        public static int CalculateAge(DateTime dtDateOfBirth)
        {
            int iAge = DateTime.Today.Year - dtDateOfBirth.Year;

            if (dtDateOfBirth.Date > DateTime.Today.AddYears(-iAge))
                iAge--;

            return iAge;
        }
    }
}
