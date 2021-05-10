using Grupp4Lms.Core.Entities;
using Grupp4Lms.Core.IRepositories;
using Grupp4Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupp4Lms.Data.Repositories
{
    /// <summary>
    /// KurslitteraturRepository med metoder för att hämta Forfattare och Litteratur
    /// </summary>
    public class KurslitteraturRepository : IKurslitteraturRepository
    {
        /// <summary>
        /// Databas context
        /// </summary>
        private readonly ApplicationDbContext m_DbContext;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Referense till context</param>
        public KurslitteraturRepository(ApplicationDbContext context)
        {
            m_DbContext = context;
        }


        /// <summary>
        /// Async metod som returnerar sökt litteratur exklusive författare
        /// </summary>
        /// <param name="id">Id för sökt litteratur</param>
        /// <returns>Task med sökt litteratur exklusive författare eller null</returns>
        public async Task<Litteratur> GetLittaraturAsync(int id)
        {
            return await m_DbContext.Litteratur
                .Include(n => n.Niva)
                .Include(a => a.Amne)
                .Where(l => l.LitteraturId == id)
                .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Async metod som returnerar litteratur exklusive författare
        /// </summary>
        /// <returns>IEnumerable med Litteratur exklusive författare</returns>
        public async Task<IEnumerable<Litteratur>> GetLitteraturAsync()
        {
            return await m_DbContext.Litteratur
                .Include(n => n.Niva)
                .Include(a => a.Amne)
                .ToListAsync();
        }

        /// <summary>
        /// Async metod som returnerar litteratur inklusive författare
        /// </summary>
        /// <returns>IEnumerable med Litteratur inklusive författare</returns>
        public async Task<IEnumerable<Litteratur>> GetLitteraturInklusiveForfattareAsync()
        {
            return await m_DbContext.Litteratur
                .Include(n => n.Niva)
                .Include(a => a.Amne)
                .Include(f => f.Forfattare)
                .ToListAsync(); ;
        }

        /// <summary>
        /// Async metod som returnerar sökt litteratur inklusive författare
        /// </summary>
        /// <param name="id">Id för sökt litteratur</param>
        /// <returns>Task med sökt litteratur inklusive författare eller null</returns>
        public async Task<Litteratur> GetLitteraturInklusiveForfattareAsync(int id)
        {
            return await m_DbContext.Litteratur
                .Include(n => n.Niva)
                .Include(a => a.Amne)
                .Include(f => f.Forfattare)
                .Where(l => l.LitteraturId == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Async metod som sparar ändringar
        /// </summary>
        /// <returns>true om några ändringar sparas. Annars returneras false</returns>
        public async Task<bool> SaveAsync()
        {
            return (await m_DbContext.SaveChangesAsync()) >= 0;
        }
    }
}
