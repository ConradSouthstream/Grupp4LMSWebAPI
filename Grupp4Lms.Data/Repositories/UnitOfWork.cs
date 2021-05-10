using Grupp4Lms.Core.IRepositories;
using Grupp4Lms.Data.Data;
using System.Threading.Tasks;

namespace Grupp4Lms.Data.Repositories
{
    /// <summary>
    /// Unit of work objektet som har alla repositories
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Databas context
        /// </summary>
        private readonly ApplicationDbContext m_dbContext;

        /// <summary>
        /// KurslitteraturRepository
        /// </summary>
        public IKurslitteraturRepository KurslitteraturRepository { get; private set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Referense till context</param>
        public UnitOfWork(ApplicationDbContext context)
        {
            m_dbContext = context;
            KurslitteraturRepository = new KurslitteraturRepository(m_dbContext);
        }


        /// <summary>
        /// Async metod som skall spara uppdaterad data i repositories
        /// </summary>
        /// <returns>Task</returns>
        public async Task CompleteAsync()
        {
            await KurslitteraturRepository.SaveAsync();
        }
    }
}