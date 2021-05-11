using Grupp4Lms.Core.Entities;
using Grupp4Lms.Core.IRepositories;
using Grupp4Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
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
        /// Async metod som returnerar alla nivåer
        /// </summary>
        /// <returns>IEnumerable med alla nivåer</returns>
        public async Task<IEnumerable<Niva>> GetNivaerAsync()
        {
            return await m_DbContext.Niva.ToListAsync();
        }


        /// <summary>
        /// Async metod som returnerar alla ämnen
        /// </summary>
        /// <returns>IEnumerable med alla ämnen</returns>
        public async Task<IEnumerable<Amne>> GetAmnenAsync()
        {
            return await m_DbContext.Amne.ToListAsync();
        }


        /// <summary>
        /// Async metod som returnerar alla författare
        /// </summary>
        /// <returns>IEnumerable med författare</returns>
        public async Task<IEnumerable<Forfattare>> GetForfattareAsync()
        {
            return await m_DbContext.Forfattar.ToListAsync();
        }


        /// <summary>
        /// Async metod som returnerar sökt författare
        /// </summary>
        /// <param name="id">Id för sökt författare</param>
        /// <returns>Task med sökt författare</returns>
        public async Task<Forfattare> GetForfattareAsync(int id)
        {
            return await m_DbContext.Forfattar
                .Where(i => i.ForfatterId == id)
                .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Async metod som returnerar alla författare inklusive litteratur som författaren har varit med och skrivit
        /// </summary>
        /// <returns>IEnumerable med författare inklusive litteratur som författaren har varit med och skrivit</returns>
        public async Task<IEnumerable<Forfattare>> GetForfattareInklusiveLitteraturAsync()
        {
            return await m_DbContext.Forfattar
                .Include(l => l.Litteratur)
                .ThenInclude(a => a.Amne)
                .Include(l => l.Litteratur)
                .ThenInclude(n => n.Niva)
                .ToListAsync();
        }


        /// <summary>
        /// Async metod som returnerar en sökt författare inklusive litteratur som författaren har varit med och skrivit
        /// </summary>
        /// <param name="id">id för sökt författare</param>
        /// <returns>Task med sökt författare inklusive litteratur som författaren har varit med och skrivit</returns>
        public async Task<Forfattare> GetForfattareInklusiveLitteraturAsync(int id)
        {
            return await m_DbContext.Forfattar
                .Include(l => l.Litteratur)
                .ThenInclude(a => a.Amne)
                .Include(l => l.Litteratur)
                .ThenInclude(n => n.Niva)
                .Where(i => i.ForfatterId == id)
                .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Async metod som söker efter litteratur som matchar de olika sök parametrarna
        /// </summary>
        /// <param name="strTitel">Titel som vi vill söka på. Kontrollerar om titel innehåller sökt text. Kan vara null eller empty string</param>
        /// <param name="strForfattare">Namn på författaren som vi vill söka på. Kontrollerar om förnamn eller efternamn innehåller det som söks. Kan vara null eller empty string</param>
        /// <param name="iAmne">Id för ämne som vi vill söka på. Default är -1 och då söker vi inte på ämne</param>
        /// <returns>Task IEnumerable med Litteratur inklusive författare</returns>
        public async Task<IEnumerable<Litteratur>> SearchLitteraturInklusiveForfattareAsyn(string strTitel, string strForfattare, int iAmne = -1)
        {
            bool bFiltreradePaForfattare = false;
            bool bFiltreradPaTitel = false;
            bool bFiltreradPaAmne = false;

            List<Litteratur> lsLitteratur = null;

            if (!String.IsNullOrWhiteSpace(strTitel))
            {// Vi har titel. Filtrera på det

                strTitel = strTitel.ToLower();
                strTitel = strTitel.Trim();

                try
                {
                    lsLitteratur = await m_DbContext.Litteratur
                        .Include(n => n.Niva)
                        .Include(a => a.Amne)
                        .Include(f => f.Forfattare)
                        .Where(t => t.Titel.ToLower().Contains(strTitel))
                        .ToListAsync();
                }
                catch(Exception exc)
                {}

                bFiltreradPaTitel = true;
            }


            if (iAmne > 0)
            {// Vi har ämne. Filtrera på det

                if(bFiltreradPaTitel)
                {// Vi har filtrerat på titel
                    lsLitteratur = lsLitteratur?.Where(a => a.AmneId == iAmne).ToList();
                }
                else
                {
                    try
                    {
                        lsLitteratur = await m_DbContext.Litteratur
                            .Include(n => n.Niva)
                            .Include(a => a.Amne)
                            .Include(f => f.Forfattare)
                            .Where(a => a.AmneId == iAmne)
                            .ToListAsync();
                    }
                    catch(Exception exc) 
                    { }
                }

                bFiltreradPaAmne = true;
            }


            if (!String.IsNullOrWhiteSpace(strForfattare))
            {// Vi skall filtrera på författare

                List<int> lsForfattareId = null;
                strForfattare = strForfattare.ToLower();
                strForfattare = strForfattare.Trim();

                // Nu vill vi ha en lista med författare filtrerade på för- och efternamn
                try
                {
                    lsForfattareId = await m_DbContext.Forfattar
                        .Where(f => f.ForNamn.ToLower().Contains(strForfattare) || f.EfterNamn.ToLower().Contains(strForfattare))
                        .Select(i => i.ForfatterId)
                        .Distinct()
                        .ToListAsync();
                }
                catch(Exception exc) 
                { }

                if (!bFiltreradPaTitel && !bFiltreradPaAmne)
                {// Vi har inte filtrerat något. Hämta all litteratur från db

                    try
                    {
                        lsLitteratur = await m_DbContext.Litteratur
                            .Include(n => n.Niva)
                            .Include(a => a.Amne)
                            .Include(f => f.Forfattare)
                            .ToListAsync();
                    }
                    catch (Exception exc) 
                    { }
                }


                if(lsLitteratur?.Count > 0 && lsForfattareId?.Count > 0)
                {// Nu har jag en lista med litteratur och en lista med författares id som vi skall filtrerar med

                    int? iAntalForfattare = 0;
                    List<Litteratur> lsTmpLitteratur = new List<Litteratur>();

                    foreach(var litteratur in lsLitteratur)
                    {
                        iAntalForfattare = 0;
                        if (litteratur.Forfattare?.Count > 0)
                        {
                            iAntalForfattare = litteratur.Forfattare
                                .Where(i => lsForfattareId.Contains(i.ForfatterId))?.Count();

                            if(iAntalForfattare.HasValue && iAntalForfattare.Value > 0)
                                lsTmpLitteratur.Add(litteratur);
                        }
                    }

                    lsLitteratur = lsTmpLitteratur;
                    bFiltreradePaForfattare = true;
                }                
            }


            if (!bFiltreradePaForfattare && !bFiltreradPaTitel && !bFiltreradPaAmne)
            {// Vi har inte filtrerat på något. Returnera allt

                try
                {
                    lsLitteratur = await m_DbContext.Litteratur
                        .Include(n => n.Niva)
                        .Include(a => a.Amne)
                        .Include(f => f.Forfattare)
                        .ToListAsync();
                }
                catch(Exception exc) 
                { }
            }
            else
            {// Vi har filtrerat. Se till att vi inte har några dubbletter
                if(lsLitteratur?.Count > 1)
                    lsLitteratur = lsLitteratur.Distinct().ToList();
            }

            return lsLitteratur;
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
