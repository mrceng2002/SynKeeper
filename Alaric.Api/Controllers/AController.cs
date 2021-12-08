using Alaric.DB;
using Alaric.DB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alaric.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AController(AppDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
        }
        // GET: api/<AController>
        [HttpGet]
        public IEnumerable<AModel> Get()
        {
            return _context.AModels.ToList().OrderByDescending(p => p.modifyDt);
        }

        // GET api/<AController>/5
        [HttpGet("{id}")]
        public AModel Get(int id)
        {
            return _context.AModels.Find(id);
        }

        // POST api/<AController>
        [HttpPost]
        public void Post([FromBody] AModel pAModel)
        {
            pAModel.modifyDt = DateTime.Now;
            _context.AModels.Add(pAModel);
            _context.SaveChanges();
        }

        // PUT api/<AController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AModel value)
        {
            if (id!=value.Id)
            {
                return BadRequest("Wrong Id error");
            }

            value.modifyDt = DateTime.Now;

            AModel model = _context.AModels.Find(id);
            if (model==null)
            {
                return NotFound("Sent value not found");
            }

            model.Id = value.Id;
            model.LocalId = value.LocalId;
            model.modifyDt = DateTime.Now;
            model.partId = value.partId;
            model.StringId = value.StringId;
            model.Sym = value.Sym;
            model.tradePrice = value.tradePrice;
            model.tradeSize = value.tradeSize;

            _context.SaveChanges();

            return Ok(value);
        }

        // DELETE api/<AController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return UnprocessableEntity();
        }
    }
}
