using AutoMapper;
using Grupp4Lms.Core.Dto;
using Grupp4Lms.Core.Entities;
using Grupp4Lms.Core.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Grupp4Lms.Api.Controllers
{
    /// <summary>
    /// KurslitteraturController med action för Litteratur, Forfattare, Amnen och Nivåer
    /// </summary>
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class KurslitteraturController : ControllerBase
    {
        private readonly IUnitOfWork m_Uow;
        private readonly IMapper m_Mapper;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uow">Unit of work. Används för att anropa olika Repository</param>
        /// <param name="mapper">Automapper</param>
        public KurslitteraturController(IUnitOfWork uow, IMapper mapper)
        {
            m_Uow = uow;
            m_Mapper = mapper;
        }

        #region Action för att hämta nivåer

        // GET api/<KurslitteraturController>/GetNivaer
        /// <summary>
        /// GET: api/KurslitteraturController/GetNivaer
        /// Async metod som returnerar IEnumarable med Nivåer
        /// </summary>
        /// <returns>Ok=200 och en lista med Nivåer</returns>
        /// <response code="200">Returnerade lista med Nivåer i form av NivaDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NivaDto))]
        [HttpGet("GetNivaer")]
        public async Task<ActionResult<IEnumerable<NivaDto>>>GetNivaer()
        {
            List<NivaDto> lsNivaer = null;
            NivaDto dto = null;

            // Hämta nivåerna från repository
            var nivaer = await m_Uow.KurslitteraturRepository.GetNivaerAsync();
            if(nivaer != null && nivaer.Count() > 0)
            {
                // Map Niva till NivaDto
                lsNivaer = new List<NivaDto>(nivaer.Count());
                foreach(Niva niva in nivaer)
                {
                    dto = m_Mapper.Map<NivaDto>(niva);
                    if(dto != null)
                        lsNivaer.Add(dto);
                }
            }

            return Ok(lsNivaer);
        }

        #endregion // End of region Action för att hämta nivåer

        #region Action för att hämta Ämnen

        // GET api/<KurslitteraturController>/GetAmnen
        /// <summary>
        /// GET: api/KurslitteraturController/GetAmnen
        /// Async metod som returnerar IEnumarable med Ämnen
        /// </summary>
        /// <returns>Ok=200 och en lista med Ämnen</returns>
        /// <response code="200">Returnerade lista med Ämnen i form av AmneDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AmneDto))]
        [HttpGet("GetAmnen")]
        public async Task<ActionResult<IEnumerable<AmneDto>>>GetAmnen()
        {
            List<AmneDto> lsAmnen = null;
            AmneDto dto = null;

            // Hämta alla ämnen från repository
            var amnen = await m_Uow.KurslitteraturRepository.GetAmnenAsync();
            if(amnen != null && amnen.Count() > 0)
            {
                // Map Amne till AmneDto
                lsAmnen = new List<AmneDto>(amnen.Count());
                foreach (Amne amne in amnen)
                {
                    dto = m_Mapper.Map<AmneDto>(amne);
                    if (dto != null)
                        lsAmnen.Add(dto);
                }
            }

            return Ok(lsAmnen);
        }

        #endregion // End of region Action för att hämta Ämnen

        #region Action för att hämta litteratur exklusive författare

        // GET api/<KurslitteraturController>/GetLitteraturen
        /// <summary>
        /// GET: api/KurslitteraturController/GetLitteraturen
        /// Async metod som returnerar litteraturen exklusive forfattare
        /// </summary>
        /// <returns>Ok=200 och en lista med litteratur exklusive forfattare</returns>
        /// <response code="200">Returnerade lista med LitteraturDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LitteraturDto))]
        [HttpGet("GetLitteraturen")]
        public async Task<ActionResult<IEnumerable<LitteraturDto>>> GetLitteraturen()
        {
            List<LitteraturDto> lsLitteraturDto = null;
            LitteraturDto dto = null;

            // Hämta kurslitteratur
            var litteraturen = await m_Uow.KurslitteraturRepository.GetLitteraturAsync();
            if (litteraturen != null && litteraturen.Count() > 0)
            {
                // Map litteratur till LitteraturDto
                lsLitteraturDto = new List<LitteraturDto>(litteraturen.Count());
                foreach (Litteratur lr in litteraturen)
                {
                    dto = m_Mapper.Map<LitteraturDto>(lr);
                    if (dto != null)
                        lsLitteraturDto.Add(dto);
                }
            }

            return Ok(lsLitteraturDto);
        }


        // GET api/<KurslitteraturController>/GetLitteratur/1
        /// <summary>
        /// GET: api/KurslitteraturController/GetLitteratur/1
        /// Async metod som returnerar sökt litteratur exklusive forfattare
        /// </summary>
        /// <param name="id">Id för sökt litteratur</param>
        /// <returns>Ok=200 och sökt litteratur exklusive forfattare</returns>
        /// <response code="200">Returnerar sökt litteratur som ett LitteraturDto objekt</response>
        /// <response code="404">Hittade inte sökt litteratur</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LitteraturDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetLitteratur/{id}")]
        public async Task<ActionResult<LitteraturDto>> GetLitteratur(int id)
        {
            // Hämta sökt kurslitteratur
            var litteratur = await m_Uow.KurslitteraturRepository.GetLittaraturAsync(id);

            if (litteratur == null)
                return NotFound();

            // Map litteratur till LitteraturDto
            LitteraturDto dto = m_Mapper.Map<LitteraturDto>(litteratur);

            return Ok(dto);
        }

        #endregion // End of region Action för att hämta litteratur exklusive författare

        #region Action för att hämta litteratur inklusive författare

        // GET api/<KurslitteraturController>/GetLitteraturenInklusiveForfattare
        /// <summary>
        /// GET: api/KurslitteraturController/GetLitteraturenInklusiveForfattare
        /// Async metod som returnerar litteraturen exklusive forfattare
        /// </summary>
        /// <returns>Ok=200 och en lista med litteratur inklusive forfattare</returns>
        /// <response code="200">Returnerade lista med LitteraturInklusiveForfattareDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LitteraturInklusiveForfattareDto))]
        [HttpGet("GetLitteraturenInklusiveForfattare")]
        public async Task<ActionResult<IEnumerable<LitteraturInklusiveForfattareDto>>> GetLitteraturenInklusiveForfattare()
        {
            List<LitteraturInklusiveForfattareDto> lsLitteraturDto = null;
            LitteraturInklusiveForfattareDto dto = null;

            // Hämta litteratur
            var litteratur = await m_Uow.KurslitteraturRepository.GetLitteraturInklusiveForfattareAsync();
            if (litteratur != null && litteratur.Count() > 0)
            {
                // Map litteratur till LitteraturInklusiveForfattareDto
                lsLitteraturDto = new List<LitteraturInklusiveForfattareDto>(litteratur.Count());
                foreach (Litteratur lr in litteratur)
                {
                    dto = m_Mapper.Map<LitteraturInklusiveForfattareDto>(lr);
                    if (dto != null)
                        lsLitteraturDto.Add(dto);
                }
            }

            return Ok(lsLitteraturDto);
        }


        // GET api/<KurslitteraturController>/GetLitteraturInklusiveForfattare/1
        /// <summary>
        /// GET: api/KurslitteraturController/GetLitteraturInklusiveForfattare/1
        /// Async metod som returnerar sökt litteratur inklusive forfattare
        /// </summary>
        /// <param name="id">id för sökt litteratur</param>
        /// <returns>Ok=200 och sökt litteratur inklusive forfattare</returns>
        /// <response code="200">Returnerade sökt litteratur som ett LitteraturInklusiveForfattareDto objekt</response>
        /// <response code="404">Hittade inte sökt litteratur</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LitteraturInklusiveForfattareDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetLitteraturInklusiveForfattare/{id}")]
        public async Task<ActionResult<LitteraturInklusiveForfattareDto>> GetLitteraturInklusiveForfattare(int id)
        {
            // Hämta litteratur
            var litteratur = await m_Uow.KurslitteraturRepository.GetLitteraturInklusiveForfattareAsync(id);

            if (litteratur == null)
                return NotFound();

            // Map litteratur till LitteraturInklusiveForfattareDto
            LitteraturInklusiveForfattareDto dto = m_Mapper.Map<LitteraturInklusiveForfattareDto>(litteratur);

            return Ok(dto);
        }

        #endregion  // End of region Action för att hämta litteratur inklusive författare

        #region Action för att hämta författare

        // GET api/<KurslitteraturController>/GetForfattare
        /// <summary>
        /// GET: api/KurslitteraturController/GetForfattare
        /// Async metod som returnerar författarna
        /// </summary>
        /// <returns>Ok=200 och en lista med författare</returns>
        /// <response code="200">Returnerade lista med författare som  ForfattareDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForfattareDto))]
        [HttpGet("GetForfattare")]
        public async Task<ActionResult<IEnumerable<ForfattareDto>>> GetForfattare()
        {
            List<ForfattareDto> lsForfattare = null;
            ForfattareDto dto = null;

            // Hämta författare
            var forfattare = await m_Uow.KurslitteraturRepository.GetForfattareAsync();
            if(forfattare != null && forfattare.Count() > 0)
            {
                // Map forfattare till ForfattareDto
                lsForfattare = new List<ForfattareDto>(forfattare.Count());
                foreach(Forfattare fe in forfattare)
                {
                    dto = m_Mapper.Map<ForfattareDto>(fe);
                    if (dto != null)
                        lsForfattare.Add(dto);
                }
            }

            return Ok(lsForfattare);
        }

        // GET api/<KurslitteraturController>/GetForfattare/1
        /// <summary>
        /// GET: api/KurslitteraturController/GetForfattare/1
        /// Async metod som returnerar sökt författare
        /// </summary>
        /// <param name="id">id för sökt författare</param>
        /// <returns>Ok=200 och sökt författare</returns>
        /// <response code="200">Returnerar sökt författare som ForfattareDto objekt</response>
        /// <response code="404">Hittade inte sökt litteratur</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForfattareDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetForfattare/{id}")]
        public async Task<ActionResult<ForfattareDto>> GetForfattare(int id)
        {
            // Hämta författare 
            var forfattare = await m_Uow.KurslitteraturRepository.GetForfattareAsync(id);

            if (forfattare == null)
                return NotFound();

            // Map författare till ForfattareDto
            ForfattareDto dto = m_Mapper.Map<ForfattareDto>(forfattare);

            return Ok(dto);
        }


        #endregion // End of region Action för att hämta författare

        #region Action för att hämta författare inklusive litteratur


        // GET api/<KurslitteraturController>/GetForfattareInklusiveLitteratur
        /// <summary>
        /// GET: api/KurslitteraturController/GetForfattareInklusiveLitteratur
        /// Async metod som returnerar författarna inklusive litteratur som författaren har varit med och skrivit
        /// </summary>
        /// <returns>Ok=200 och en lista med författare inklusive litteratur som författaren har varit med och skrivit</returns>
        /// <response code="200">Returnerade lista med författare inklusive litteratur som författaren har varit med och skrivit som ForfattareInklusiveLitteraturDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForfattareInklusiveLitteraturDto))]
        [HttpGet("GetForfattareInklusiveLitteratur")]
        public async Task<ActionResult<IEnumerable<ForfattareInklusiveLitteraturDto>>> GetForfattareInklusiveLitteratur()
        {
            List<ForfattareInklusiveLitteraturDto> lsForfattare = null;
            ForfattareInklusiveLitteraturDto dto = null;

            // Hämta författare
            var forfattare = await m_Uow.KurslitteraturRepository.GetForfattareInklusiveLitteraturAsync();
            if (forfattare != null && forfattare.Count() > 0)
            {
                // Map forfattare till ForfattareInklusiveLitteraturDto
                lsForfattare = new List<ForfattareInklusiveLitteraturDto>(forfattare.Count());
                foreach (Forfattare fe in forfattare)
                {
                    dto = m_Mapper.Map<ForfattareInklusiveLitteraturDto>(fe);
                    if (dto != null)
                        lsForfattare.Add(dto);
                }
            }

            return Ok(lsForfattare);
        }


        // GET api/<KurslitteraturController>/GetForfattareInkusiveLitteratur/1
        /// <summary>
        /// GET: api/KurslitteraturController/GetForfattareInkusiveLitteratur/1
        /// Async metod som returnerar sökt författare inklusive den litteratur som författaren har skrivit
        /// </summary>
        /// <param name="id">id för sökt författare</param>
        /// <returns>Ok=200 och sökt författare inklusive den litteratur som författaren har skrivit</returns>
        /// <response code="200">Returnerade sökt författare inklusive den litteratur som författaren har skrivit som ForfattareInklusiveLitteraturDto objekt</response>
        /// <response code="404">Hittade inte sökt litteratur</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForfattareInklusiveLitteraturDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetForfattareInkusiveLitteratur/{id}")]
        public async Task<ActionResult<ForfattareInklusiveLitteraturDto>> GetForfattareInkusiveLitteratur(int id)
        {
            // Hämta författare inklusive litteratur som författaren har skrivit
            var forfattare = await m_Uow.KurslitteraturRepository.GetForfattareInklusiveLitteraturAsync(id);

            if (forfattare == null)
                return NotFound();

            // Map författare till ForfattareInklusiveLitteraturDto
            ForfattareInklusiveLitteraturDto dto = m_Mapper.Map<ForfattareInklusiveLitteraturDto>(forfattare);

            return Ok(dto);
        }


        #endregion // End of region Action för att hämta författare inklusive litteratur

        #region Action för sökning av kurslitteratur        

        /// <summary>
        /// Async metod som returnerar IEnumerable med Litteratur inklusive författare som matchar sökningen
        /// </summary>
        /// <param name="titel">Titel som vi vill söka på. Kan vara null eller empty string</param>
        /// <param name="forfattare">Namn på författaren som vi vill söka på. Kan vara null eller empty string</param>
        /// <param name="amne">Id för ämne som vi vill söka på. Kan vara 0 eller -1 och då söker vi inte på ämne</param>
        /// <returns>Ok=200 och IEnumerable med litteratur inklusive författare som matchar sökningen</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LitteraturInklusiveForfattareDto))]
        [HttpGet("SearchLitteratur")]
        public async Task<ActionResult<IEnumerable<LitteraturInklusiveForfattareDto>>>SearchLitteratur(string titel, string forfattare, int? amne)
        {// titel, författare eller ämnesområde
            List<LitteraturInklusiveForfattareDto> lsLitteratur = null;
            LitteraturInklusiveForfattareDto dto = null;

            string strTitel = String.Empty;
            string strForfattare = String.Empty;
            int iAmne = -1;

            // Börja med att rensa upp indata
            if (!String.IsNullOrWhiteSpace(titel))
                strTitel = titel.Trim();

            if (!String.IsNullOrWhiteSpace(forfattare))
                strForfattare = forfattare.Trim();

            if (amne.HasValue)
                iAmne = amne.Value;

            // Sök efter litteratur som matchar olika parametrar som användaren vill söka på
            var litteratur = await m_Uow.KurslitteraturRepository.SearchLitteraturInklusiveForfattareAsyn(strTitel, strForfattare, iAmne);
            if(litteratur != null && litteratur.Count() > 0)
            {
                // Map från litteratur till LitteraturInklusiveForfattareDto objekt
                lsLitteratur = new List<LitteraturInklusiveForfattareDto>(litteratur.Count());
                foreach(Litteratur lr in litteratur)
                {
                    dto = m_Mapper.Map<LitteraturInklusiveForfattareDto>(lr);
                    if(dto != null)
                        lsLitteratur.Add(dto);
                }
            }

            return Ok(lsLitteratur);
        }

        #endregion // End of region Action för sökning av kurslitteratur

        #region Action för att skapa och uppdatera litteratur

        /// <summary>
        /// Async metod som skapar ny litteratur
        /// </summary>
        /// <param name="dto">CreateLitteraturDto med information om litteratur och eventuella författare</param>
        /// <returns>Om det gick bra returneras Ok = 200.
        /// Om ModelState ej är valid returneras BadRequest = 400.
        /// Om det inte gick uppdatera litteraturen returneras StatusCode = 500
        /// </returns>
        /// <response code="200">Skapandet av litteraturen gick bra</response>
        /// <response code="400">ModelState isn't valid</response>
        /// <response code="500">Det gick inte skapa litteraturen</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> PostLitteratur(CreateLitteraturDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                Litteratur litteratur = m_Mapper.Map<Litteratur>(dto);
                await m_Uow.KurslitteraturRepository.PostLitteraturAsync(litteratur);
                bool bSaveOk = await m_Uow.KurslitteraturRepository.SaveAsync();

                if (!bSaveOk)
                    return StatusCode(500);
            }
            catch(Exception exc)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        #endregion // End of region Action för att ladda upp kurslitteratur

        #region Action för att uppdatera information om litteratur

        /// <summary>
        /// PUT: api/KursLitteratur/5
        /// Put, update/replace, litteratur
        /// </summary>
        /// <param name="id">id för litteratur som skall uppdateras</param>
        /// <param name="dto">Information om litteraturen som skall uppdateras</param>
        /// <returns>Om det gick bra returneras Ok = 200. 
        /// Om id och CreateLitteraturDto ej har samma id returneras BadRequest = 400. 
        /// Om ModelState ej är valid returneras BadRequest = 400.
        /// Om litteraturen som skall uppdateras inte finns returneras NotFound = 404.
        /// Om det inte gick uppdatera litteraturen returneras StatusCode = 500
        /// </returns>
        /// <response code="200">Uppdatering av litteraturen gick bra</response>
        /// <response code="400">Id och litteraturen id är inte samma eller ModelState isn't valid</response>
        /// <response code="404">Litteraturen som skall uppdateras finns ej</response>
        /// <response code="500">Det gick inte uppdatera informationen om litteraturen</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("PutLitteraturAsync/{id}")]
        public async Task<IActionResult> PutLitteraturAsync(int id, EditLitteraturDto dto)
        {
            if (id != dto.LitteraturId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if(dto == null)
                return BadRequest();

            try
            {
                // Map EditLitteraturDto till Litteratur
                Litteratur litteratur = m_Mapper.Map<Litteratur>(dto);

                m_Uow.KurslitteraturRepository.PutLitteratur(litteratur);
                bool bSaveOk = await m_Uow.KurslitteraturRepository.SaveAsync();

                if (!bSaveOk)
                    return StatusCode(500);
            }
            catch (DbUpdateConcurrencyException)
            {
                bool bCourseExists = await m_Uow.KurslitteraturRepository.LitteraturExistsAsync(id);
                if (!bCourseExists)
                    return NotFound();
                else
                    throw;
            }

            return Ok();
        }

        #endregion // End of region Action för att uppdatera information om litteratur

        #region Action för att skapa och uppdatera författare

        /// <summary>
        /// Async metod som skapar ny författare
        /// </summary>
        /// <param name="dto">CreateForfattareDto med information om forfattaren</param>
        /// <returns>Om det gick bra returneras Ok = 200.
        /// Om ModelState ej är valid returneras BadRequest = 400.
        /// Om det inte gick uppdatera forfattaren returneras StatusCode = 500
        /// </returns>
        /// <response code="200">Skapandet av forfattaren gick bra</response>
        /// <response code="400">ModelState isn't valid</response>
        /// <response code="500">Det gick inte skapa forfattaren</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("PostForfattare")]
        public async Task<ActionResult> PostForfattare(CreateForfattareDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                Forfattare forfattare = m_Mapper.Map<Forfattare>(dto);
                await m_Uow.KurslitteraturRepository.PostForfattareAsync(forfattare);
                bool bSaveOk = await m_Uow.KurslitteraturRepository.SaveAsync();

                if (!bSaveOk)
                    return StatusCode(500);
            }
            catch (Exception exc)
            {
                return StatusCode(500);
            }

            return Ok();
        }


        /// <summary>
        /// PUT: api/KursLitteratur/5
        /// Put, update/replace, forfattare
        /// </summary>
        /// <param name="id">id för forfattaren som skall uppdateras</param>
        /// <param name="dto">Information om forfattaren som skall uppdateras</param>
        /// <returns>Om det gick bra returneras Ok = 200. 
        /// Om id och EditForfattareDto ej har samma id returneras BadRequest = 400. 
        /// Om ModelState ej är valid returneras BadRequest = 400.
        /// Om forfattaren som skall uppdateras inte finns returneras NotFound = 404.
        /// Om det inte gick uppdatera författaren returneras StatusCode = 500
        /// </returns>
        /// <response code="200">Uppdatering av forfattaren gick bra</response>
        /// <response code="400">Id och forfattare id är inte samma eller ModelState isn't valid</response>
        /// <response code="404">forfattaren som skall uppdateras finns ej</response>
        /// <response code="500">Det gick inte uppdatera informationen om forfattaren</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("PutForfattareAsync/{id}")]
        public async Task<IActionResult> PutForfattareAsync(int id, EditForfattareDto dto)
        {
            if (id != dto.ForfatterId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (dto == null)
                return BadRequest();

            try
            {
                // Map EditForfattareDto till Forfattare
                Forfattare forfattare = m_Mapper.Map<Forfattare>(dto);

                m_Uow.KurslitteraturRepository.PutForfattare(forfattare);
                bool bSaveOk = await m_Uow.KurslitteraturRepository.SaveAsync();

                if (!bSaveOk)
                    return StatusCode(500);
            }
            catch (DbUpdateConcurrencyException)
            {
                bool bCourseExists = await m_Uow.KurslitteraturRepository.ForfattareExistsAsync(id);
                if (!bCourseExists)
                    return NotFound();
                else
                    throw;
            }

            return Ok();
        }

        #endregion // End region Action för att skapa och uppdatera författare


        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<KurslitteraturController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<KurslitteraturController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<KurslitteraturController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<KurslitteraturController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
