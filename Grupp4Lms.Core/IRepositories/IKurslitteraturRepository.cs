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

        /// <summary>
        /// Async metod som returnerar alla nivåer
        /// </summary>
        /// <returns>IEnumerable med alla nivåer</returns>
        Task<IEnumerable<Niva>> GetNivaerAsync();

        /// <summary>
        /// Async metod som returnerar alla ämnen
        /// </summary>
        /// <returns>Task IEnumerable med alla ämnen</returns>
        Task<IEnumerable<Amne>> GetAmnenAsync();

        /// <summary>
        /// Async metod som returnerar alla författare
        /// </summary>
        /// <returns>Task IEnumerable med författare</returns>
        Task<IEnumerable<Forfattare>> GetForfattareAsync();

        /// <summary>
        /// Async metod som returnerar sökt författare
        /// </summary>
        /// <param name="id">Id för sökt författare</param>
        /// <returns>Task med sökt författare</returns>
        Task<Forfattare> GetForfattareAsync(int id);

        /// <summary>
        /// Async metod som returnerar alla författare inklusive litteratur som författaren har varit med och skrivit
        /// </summary>
        /// <returns>Task IEnumerable med författare inklusive litteratur som författaren har varit med och skrivit</returns>
        Task<IEnumerable<Forfattare>> GetForfattareInklusiveLitteraturAsync();

        /// <summary>
        /// Async metod som returnerar en sökt författare inklusive litteratur som författaren har varit med och skrivit
        /// </summary>
        /// <param name="id">id för sökt författare</param>
        /// <returns>Task med sökt författare inklusive litteratur som författaren har varit med och skrivit</returns>
        Task<Forfattare> GetForfattareInklusiveLitteraturAsync(int id);

        /// <summary>
        /// Async metod som söker efter litteratur som matchar de olika sökt pararmetrarna
        /// </summary>
        /// <param name="strTitel">Titel som vi vill söka på. Kontrollerar om titel innehåller sökt text. Kan vara null eller empty string</param>
        /// <param name="strForfattare">Namn på författaren som vi vill söka på. Kontrollerar om förnamn eller efternamn innehåller det som söks. Kan vara null eller empty string</param>
        /// <param name="iAmne">Id för ämne som vi vill söka på. Default är -1 och då söker vi inte på ämne</param>
        /// <returns>Task IEnumerable med Litteratur inklusive författare</returns>
        Task<IEnumerable<Litteratur>> SearchLitteraturInklusiveForfattareAsyn(string strTitel, string strForfattare, int iAmne);

        /// <summary>
        /// Async metod som sparar ändringar
        /// </summary>
        /// <returns>true om några ändringar sparas. Annars returneras false</returns>
        Task<bool> SaveAsync();        
    }
}