using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Context;
using WebService.Models;
using System.Web;
using System.Data.Entity;

namespace WebService.Controllers
{
    public class OfferedPizzaController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        [HttpGet]
        [Route("api/OfferedPizza/GetAll")]
        public IQueryable<OfferedPizza> GetAll()
        {
            return db.OfferedPizzas;
        }

        [HttpGet]
        [Route("api/OfferedPizza/Get/{id}")]
        [ResponseType(typeof(OfferedPizza))]
        public IHttpActionResult Get(int id)
        {
            OfferedPizza offeredPizza = db.OfferedPizzas.Find(id);
            if (offeredPizza == null)
            {
                return BadRequest("Unknown id.");
            }

            return Ok(offeredPizza);
        }

        [HttpDelete]
        [Route("api/OfferedPizza/Delete/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            OfferedPizza offeredPizza = await db.OfferedPizzas.FindAsync(id);
            if (offeredPizza == null)
            {
                return BadRequest("Unknown id.");
            }

            db.OfferedPizzas.Remove(offeredPizza);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(OfferedPizza))]
        public async Task<IHttpActionResult> Add(OfferedPizza offeredPizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldOfferedPizzas = db.OfferedPizzas.FirstOrDefault(k => k.Name == offeredPizza.Name);
            if (oldOfferedPizzas != null)
            {
                return BadRequest("Cannot insert duplicate key row.");
            }

            if (string.IsNullOrEmpty(offeredPizza.Name))
            {
                return BadRequest("Empty name item in object!");
            }

            if (offeredPizza.Price <= 0)
            {
                return BadRequest("Price is invalid!");
            }
            //TODO dodawanie skladnikow
            db.OfferedPizzas.Add(offeredPizza);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
