using Alaric.Api.Constants;
using Alaric.Api.Helpers;
using Alaric.DB;
using Alaric.DB.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;

        public AController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
            if (contextAccessor == null)
                throw new ArgumentNullException("contextAccessor");
            _contextAccessor = contextAccessor;
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
        public async void Post([FromBody] AModel pAModel)
        {
            pAModel.modifyDt = DateTime.Now;
            _context.AModels.Add(pAModel);
            _context.SaveChanges();
            try
            {
                if (_contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host.Value == Urls.url1)
                {
                    await HttpHelper.Post<AModel>(Urls.url2, "Syn", pAModel);
                }
                else
                {
                    await HttpHelper.Post<AModel>(Urls.url1, "Syn", pAModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // PUT api/<AController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AModel value)
        {
            if (id != value.Id)
            {
                return BadRequest("Wrong Id error");
            }

            value.modifyDt = DateTime.Now;

            AModel model = _context.AModels.Find(id);
            if (model == null)
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

            try
            {
                if (_contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host.Value == Urls.url1)
                {
                    await HttpHelper.Put<AModel>(Urls.url2, "Syn", value);
                }
                else
                {
                    await HttpHelper.Put<AModel>(Urls.url1, "Syn", value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
