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
    public class IngredientController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        [HttpGet]
        [Route("api/Ingredient/GetAll")]
        public IQueryable<Ingredient> GetAll()
        {
            return db.Ingredients;
        }

        [HttpGet]
        [Route("api/Ingredient/Get/{id}")]
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult Get(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return BadRequest("Unknown id.");
            }

            return Ok(ingredient);
        }

        [HttpDelete]
        [Route("api/Ingredient/Delete/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Ingredient ingredient = await db.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return BadRequest("Unknown id.");
            }

            db.Ingredients.Remove(ingredient);

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
        [ResponseType(typeof(Ingredient))]
        public async Task<IHttpActionResult> Add(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldIngredients = db.Ingredients.FirstOrDefault(k => k.Name == ingredient.Name);
            if (oldIngredients != null)
            {
                return BadRequest("Cannot insert duplicate key row.");
            }

            if (string.IsNullOrEmpty(ingredient.Name))
            {
                return BadRequest("Empty name item in object!");
            }

            if (ingredient.Price <= 0)
            {
                return BadRequest("Price is invalid!");
            }
            //TODO aktualizowac oferowane pizze po usunieciu skladnika
            db.Ingredients.Add(ingredient);

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
