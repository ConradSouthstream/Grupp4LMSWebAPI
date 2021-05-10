using System.Threading.Tasks;

namespace Grupp4Lms.Core.IRepositories
{
    /// <summary>
    /// Interface för unit of work objektet som har alla repositories
    /// </summary>
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
