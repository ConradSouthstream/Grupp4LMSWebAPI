using System.Threading.Tasks;

namespace Grupp4Lms.Core.IRepositories
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repository för kurslitteratur
        /// </summary>
        IKurslitteraturRepository KurslitteraturRepository  { get; }

        /// <summary>
        /// Async metod som skall spara uppdaterad data i repositories
        /// </summary>
        /// <returns>Task</returns>
        Task CompleteAsync();
    }
}
