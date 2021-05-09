using Grupp4Lms.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Grupp4Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KurslitteraturController : ControllerBase
    {
        private readonly ApplicationDbContext m_Context;

        public KurslitteraturController(ApplicationDbContext context)
        {
            this.m_Context = context;
        }
        // GET: api/<KurslitteraturController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                // TODO TESTAR ATT DET FUNGERAR
                var litteratur = m_Context.Litteratur
                    .Include(a => a.Amne)
                    .Include(n => n.Niva)
                    .Include(f => f.Forfattare)
                    .ToList();

                var forfattare = m_Context.Forfattar.Include(l => l.Litteratur).ToList();

                string str = "abc";
            }
            catch(Exception exc)
            {
                string tmp = exc.ToString();

            }

            return new string[] { "value1", "value2" };
        }


        // GET api/<KurslitteraturController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<KurslitteraturController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<KurslitteraturController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<KurslitteraturController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
