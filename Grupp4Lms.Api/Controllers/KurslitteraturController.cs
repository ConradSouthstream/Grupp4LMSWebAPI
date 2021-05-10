using AutoMapper;
using Grupp4Lms.Core.Dto;
using Grupp4Lms.Core.Entities;
using Grupp4Lms.Core.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Grupp4Lms.Api.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class KurslitteraturController : ControllerBase
    {
        // TODO New version
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


        // GET api/<KurslitteraturController>/GetLitteraturen
        /// <summary>
        /// GET: api/KurslitteraturController/GetLitteraturen
        /// Async metod som returnerar litteraturen exklusive forfattare
        /// </summary>
        /// <returns>Ok=200 och en lista med litteratur exklusive forfattare</returns>
        /// <response code="200">Returnerade lista med LitteraturDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LitteraturDto))]
        //[Route("GetLitteraturen")]
        [HttpGet("GetLitteraturen")]
        public async Task<ActionResult<IEnumerable<LitteraturDto>>> GetLitteraturen()
        {
            List<LitteraturDto> lsLitteraturDto = null;
            LitteraturDto dto = null;

            // Hämta kurslitteratur
            var litteraturen = await m_Uow.KurslitteraturRepository.GetLitteraturAsync();
            if (litteraturen != null && litteraturen.Count() > 0)
            {
                // Map kurslitteratur till dto
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
        //[Route("GetLitteratur")]
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


        // GET api/<KurslitteraturController>/GetLitteraturenInklusiveForfattare
        /// <summary>
        /// GET: api/KurslitteraturController/GetLitteraturenInklusiveForfattare
        /// Async metod som returnerar litteraturen exklusive forfattare
        /// </summary>
        /// <returns>Ok=200 och en lista med litteratur inklusive forfattare</returns>
        /// <response code="200">Returnerade lista med LitteraturInklusiveForfattareDto objekt</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LitteraturInklusiveForfattareDto))]
        //[Route("GetLitteraturenInklusiveForfattare")]
        [HttpGet("GetLitteraturenInklusiveForfattare")]
        public async Task<ActionResult<IEnumerable<LitteraturInklusiveForfattareDto>>> GetLitteraturenInklusiveForfattare()
        {
            List<LitteraturInklusiveForfattareDto> lsLitteraturDto = null;
            LitteraturInklusiveForfattareDto dto = null;

            // Hämta kurslitteratur
            var litteratur = await m_Uow.KurslitteraturRepository.GetLitteraturInklusiveForfattareAsync();
            if (litteratur != null && litteratur.Count() > 0)
            {
                // Map kurslitteratur till dto
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
        //[Route("GetLitteraturInklusiveForfattare")]
        [HttpGet("GetLitteraturInklusiveForfattare/{id}")]
        public async Task<ActionResult<LitteraturInklusiveForfattareDto>> GetLitteraturInklusiveForfattare(int id)
        {
            // Hämta kurslitteratur
            var litteratur = await m_Uow.KurslitteraturRepository.GetLitteraturInklusiveForfattareAsync(id);

            if (litteratur == null)
                return NotFound();

            // Map till LitteraturInklusiveForfattareDto
            LitteraturInklusiveForfattareDto dto = m_Mapper.Map<LitteraturInklusiveForfattareDto>(litteratur);

            return Ok(dto);
        }



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
