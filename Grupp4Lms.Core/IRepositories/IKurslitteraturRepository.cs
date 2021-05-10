using Grupp4Lms.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grupp4Lms.Core.IRepositories
{
    /// <summary>
    /// Interface för KurslitteraturRepository
    /// </summary>
    public interface IKurslitteraturRepository
    {
        /// <summary>
        /// Async metod som sparar ändringar
        /// </summary>
        /// <returns>true om några ändringar sparas. Annars returneras false</returns>
        Task<bool> SaveAsync();

        /// <summary>
        /// Async metod som returnerar litteratur exklusive författare
        /// </summary>
        /// <returns>Task IEnumerable med Litteratur exklusive författare</returns>
        Task<IEnumerable<Litteratur>> GetLitteraturAsync();

        /// <summary>
        /// Async metod som returnerar sökt litteratur exklusive författare
        /// </summary>
        /// <param name="id">Id för sökt litteratur</param>
        /// <returns>Task med sökt litteratur exklusive författare eller null</returns>
        Task<Litteratur> GetLittaraturAsync(int id);

        /// <summary>
        /// Async metod som returnerar litteratur inklusive författare
        /// </summary>
        /// <returns>Task IEnumerable med Litteratur inklusive författare</returns>
        Task<IEnumerable<Litteratur>> GetLitteraturInklusiveForfattareAsync();

        /// <summary>
        /// Async metod som returnerar sökt litteratur inklusive författare
        /// </summary>
        /// <param name="id">Id för sökt litteratur</param>
        /// <returns>Task med sökt litteratur inklusive författare eller null</returns>
        Task<Litteratur>GetLitteraturInklusiveForfattareAsync(int id);        
    }
}