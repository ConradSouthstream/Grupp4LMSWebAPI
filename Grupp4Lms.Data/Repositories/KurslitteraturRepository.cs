﻿using Grupp4Lms.Core.Entities;
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
        /// Async metod som sparar en ny litteratur, inklusive författare i databasen. Det behöver inte finnas några författare
        /// </summary>
        /// <param name="litteratur">Den nya litteraturen som även kan innehålla författare</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Litteratur objektet är null</exception>
        public async Task PostLitteraturAsync(Litteratur litteratur)
        {
            if (litteratur is null)
                throw new ArgumentNullException("KurslitteraturRepository. PostLitteraturAsync. Reference to litteratur is null");

            await m_DbContext.Litteratur.AddAsync(litteratur);
        }


        /// <summary>
        /// Metod som uppdaterar information om litteratur
        /// </summary>
        /// <param name="litteratur">Litteratur som vi skall uppdatera</param>
        /// <returns>void</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Litteratur objektet är null</exception>
        public void PutLitteratur(Litteratur litteratur)
        {
            if (litteratur is null)
                throw new ArgumentNullException("KurslitteraturRepository. PutLitteratur. Reference to litteratur is null");

            m_DbContext.Entry(litteratur).State = EntityState.Modified;
        }


        /// <summary>
        /// Async metod som raderar litteratur
        /// </summary>
        /// <param name="litteratur">litteratur som skall raderas</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Litteratur objektet är null</exception>
        public async Task DeleteLitteraturAsync(Litteratur litteratur)
        {
            if (litteratur is null)
                throw new ArgumentNullException("KurslitteraturRepository. DeleteLitteraturAsync. Reference to Litteratur is null");

            // TODO. Fungerar inte riktigt. Raderar inte författarna från databasen
            m_DbContext.Litteratur.Remove(litteratur);
        }


        /// <summary>
        /// Async metod som kontrollerar om litteraturen finns
        /// </summary>
        /// <param name="id">Id för litteraturen som vi söker</param>
        /// <returns>true om sökt literatur finns. Annars returneras false</returns>
        public async Task<bool> LitteraturExistsAsync(int id)
        {
            bool bLitteraturExists = true;

            var litteratur = await m_DbContext.Litteratur.Where(l => l.LitteraturId == id).FirstOrDefaultAsync();
            if (litteratur is null)
                bLitteraturExists = false;

            return bLitteraturExists;
        }


        /// <summary>
        /// Metod som uppdaterar information om en författare
        /// </summary>
        /// <param name="forfattare">Författare som skall uppdateras</param>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        public void PutForfattare(Forfattare forfattare)
        {
            if (forfattare is null)
                throw new ArgumentNullException("KurslitteraturRepository. PostForfattareAsync. Reference to forfattaren is null");

            m_DbContext.Entry(forfattare).State = EntityState.Modified;
        }


        /// <summary>
        /// Async metod som skapa ett nytt forfattare objekt i repository
        /// </summary>
        /// <param name="forfattare">Den nya författaren</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        public async Task PostForfattareAsync(Forfattare forfattare)
        {
            if (forfattare is null)
                throw new ArgumentNullException("KurslitteraturRepository. PostForfattareAsync. Reference to forfattaren is null");

            await m_DbContext.Forfattar.AddAsync(forfattare);
        }


        /// <summary>
        /// Async metod som skapa ett nytt forfattare objekt i repository
        /// Kopplar författaren till litetraturen. Id för litteraturen måste finnas i propertien LitteraturId
        /// Om LitteraturId inte är större än o kommer författaren skapas utan koppling till en litteratur
        /// </summary>
        /// <param name="forfattare">Den nya författaren</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        public async Task PostForfattareKopplaTillLitteraturAsync(Forfattare forfattare)
        {
            if (forfattare is null)
                throw new ArgumentNullException("KurslitteraturRepository. PostForfattareAsync. Reference to forfattaren is null");

            if (forfattare.LitteraturId > 0)
            {
                var litteratur = await m_DbContext.Litteratur.Where(l => l.LitteraturId == forfattare.LitteraturId).FirstOrDefaultAsync();
                if (litteratur != null)
                {
                    litteratur.Forfattare = new List<Forfattare>(1);
                    litteratur.Forfattare.Add(forfattare);

                    //await m_DbContext.Forfattar.AddAsync(forfattare);
                }
            }
            else
            {
                await PostForfattareAsync(forfattare);                
            }
        }


        /// <summary>
        /// Async metod som kontrollerar om författaren finns
        /// </summary>
        /// <param name="id">Id för författaren som vi söker</param>
        /// <returns>true om sökt literatur finns. Annars returneras false</returns>
        public async Task<bool> ForfattareExistsAsync(int id)
        {
            bool bForfattareExists = true;

            var forfattare = await m_DbContext.Forfattar.Where(f => f.ForfatterId == id).FirstOrDefaultAsync();
            if (forfattare is null)
                bForfattareExists = false;

            return bForfattareExists;
        }


        /// <summary>
        /// Async metod som raderar en författare
        /// </summary>
        /// <param name="forfattare">författare som skall raderas</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Kastas om referensen till Forfattare objektet är null</exception>
        public async Task DeleteForfattareAsync(Forfattare forfattare)
        {
            if (forfattare is null)
                throw new ArgumentNullException("KurslitteraturRepository. DeleteForfattareAsync. Reference to forfattaren is null");

            m_DbContext.Forfattar.Remove(forfattare);
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
