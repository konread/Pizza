using System.Data.Entity.Infrastructure;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Context;
using WebService.Models;

namespace WebService.Controllers
{
    public class IngredientController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        // api/Ingredient/GetAll
        [HttpGet]
        [Route("api/Ingredient/GetAll")]
        public IQueryable<Ingredient> GetAll()
        {
            return db.Ingredients;
        }

        // api/Ingredient/Get/1
        [HttpGet]
        [Route("api/Ingredient/Get/{id:int}")]
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

        // api/Ingredient/Delete/1
        [HttpDelete]
        [Route("api/Ingredient/Delete/{id:int}")]
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

        // api/Ingredient/Add?name=xxx&price=yyy
        [HttpPost]
        [Route("api/Ingredient/Add")]
        [ResponseType(typeof(Ingredient))]
        public async Task<IHttpActionResult> Add(string name, double price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldIngredients = db.Ingredients.FirstOrDefault(k => k.Name == name);
            if (oldIngredients != null)
            {
                return BadRequest("Cannot insert duplicate key row.");
            }

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Empty name item in object!");
            }

            if (price <= 0)
            {
                return BadRequest("Price is invalid!");
            }
            //TODO aktualizowac oferowane pizze po usunieciu skladnika
            db.Ingredients.Add(new Ingredient() {
                Name = name,
                Price = price
            });

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
