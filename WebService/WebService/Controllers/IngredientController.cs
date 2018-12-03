using System.Data.Entity.Infrastructure;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Context;
using WebService.Models;
using System.Globalization;
using System.Threading;

namespace WebService.Controllers
{
    public class IngredientController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();
        private double basicPrice = 15.0;

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

            var distinctedListOfOfferedPizzaIds = db.IngredientsOfOfferedPizza.
                                            Where(k => k.Id_Ingredient == ingredient.Id_Ingredient).
                                            Select(k => k.Id_Offered_Pizza).
                                            Distinct().ToList();

            db.Ingredients.Remove(ingredient);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }

            if (distinctedListOfOfferedPizzaIds != null)
            {
                foreach (var offeredPizzaId in distinctedListOfOfferedPizzaIds)
                {
                    OfferedPizza offeredPizza = db.OfferedPizzas.Find(offeredPizzaId);
                    if (offeredPizza == null)
                    {
                        continue;
                    }
                    
                    var newIngredientsIds = db.IngredientsOfOfferedPizza.
                                            Where(k => k.Id_Offered_Pizza == offeredPizzaId).
                                            Select(k => k.Id_Ingredient).
                                            ToList();
                    double price = basicPrice;
                    foreach (var newIngredientId in newIngredientsIds)
                    {
                        var existedIngredients = db.Ingredients.Find(newIngredientId);
                        if (existedIngredients == null)
                        {
                            continue;
                        }

                        price += existedIngredients.Price;
                    }

                    offeredPizza.Price = price;
                }
            }

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

            name = ToTitleCase(name);

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

        private string ToTitleCase(string s)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(s);
        }
    }
}
