using Grupp4Lms.Core.Entities;
using System;
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
        /// Async metod som sparar en ny litteratur, inklusive författare i databasen. Det behöver inte finnas några författare
        /// </summary>
        /// <param name="litteratur">Den nya litteraturen som även kan innehålla författare</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Litteratur objektet är null</exception>
        Task PostLitteraturAsync(Litteratur litteratur);

        /// <summary>
        /// Metod som uppdaterar information om litteratur
        /// </summary>
        /// <param name="litteratur">Litteratur som vi skall uppdatera</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Litteratur objektet är null</exception>
        void PutLitteratur(Litteratur litteratur);

        /// <summary>
        /// Async metod som raderar litteratur
        /// </summary>
        /// <param name="litteratur">litteratur som skall raderas</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Litteratur objektet är null</exception>
        Task DeleteLitteraturAsync(Litteratur litteratur);

        /// <summary>
        /// Async metod som kontrollerar om litteraturen finns
        /// </summary>
        /// <param name="id">Id för litteraturen som vi söker</param>
        /// <returns>true om sökt literatur finns. Annars returneras false</returns>
        Task<bool> LitteraturExistsAsync(int id);

        /// <summary>
        /// Metod som uppdaterar information om en författare
        /// </summary>
        /// <param name="forfattare">Författare som skall uppdateras</param>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        void PutForfattare(Forfattare forfattare);

        /// <summary>
        /// Async metod som skapa ett nytt forfattare objekt i repository
        /// </summary>
        /// <param name="forfattare">Den nya författaren</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        Task PostForfattareAsync(Forfattare forfattare);

        /// <summary>
        /// Async metod som skapa ett nytt forfattare objekt i repository
        /// Kopplar författaren till litetraturen. Id för litteraturen måste finnas i propertien LitteraturId
        /// Om LitteraturId inte är större än o kommer författaren skapas utan koppling till en litteratur
        /// </summary>
        /// <param name="forfattare">Den nya författaren</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        Task PostForfattareKopplaTillLitteraturAsync(Forfattare forfattare);

        /// <summary>
        /// Async metod som kontrollerar om författaren finns
        /// </summary>
        /// <param name="id">Id för författaren som vi söker</param>
        /// <returns>true om sökt literatur finns. Annars returneras false</returns>
        Task<bool> ForfattareExistsAsync(int id);

        /// <summary>
        /// Async metod som raderar en författare
        /// </summary>
        /// <param name="forfattare">författare som skall raderas</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        Task DeleteForfattareAsync(Forfattare forfattare);

        /// <summary>
        /// Async metod som sparar ändringar
        /// </summary>
        /// <returns>true om några ändringar sparas. Annars returneras false</returns>
        Task<bool> SaveAsync();
    }
}