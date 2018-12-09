using System.Collections.Generic;
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
using Newtonsoft.Json.Linq;

namespace WebService.Controllers
{
    public class OfferedPizzaController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();
        private decimal basicPrice = 15.0m;

        // api/OfferedPizza/GetAll
        [HttpGet]
        [Route("api/OfferedPizza/GetAll")]
        public IHttpActionResult GetAll()
        {
            var pizza = db.OfferedPizzas;
            JObject jobject = new JObject();
            jobject.Add("OfferedPizza", JArray.FromObject(pizza));
            return Ok(jobject);
        }

        // api/OfferedPizza/GetAllWithIngredients
        [HttpGet]
        [Route("api/OfferedPizza/GetAllWithIngredients")]
        public IHttpActionResult GetAllWithIngredients()
        {
            var allPizza = db.OfferedPizzas;
            if (allPizza == null)
            {
                return BadRequest("Empty db.");
            }

            JArray response = new JArray();
            foreach (var pizza in allPizza)
            {
                JObject jobject = JObject.FromObject(pizza);
                JArray jarray = new JArray();
                var ingredientsOfOfferedPizza = db.IngredientsOfOfferedPizza.Where(k => k.Id_Offered_Pizza == pizza.Id_Offered_Pizza).ToList();
                foreach(var ingredient in ingredientsOfOfferedPizza)
                {
                    var existedIngredient = db.Ingredients.Find(ingredient.Id_Ingredient);
                    if (existedIngredient == null)
                    {
                        continue;
                    }

                    jarray.Add(JObject.FromObject(existedIngredient));
                }

                jobject.Add("Ingredients", jarray);
                response.Add(jobject);
            }

            JObject newObject = new JObject();
            newObject.Add("Pizzas", JArray.FromObject(response));
            return Ok(newObject);
        }

        // api/OfferedPizza/Get/1
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

        // api/OfferedPizza/Delete/1
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

        // api/OfferedPizza/Add?name=Marinera&ingredientsNames=sos,ser,pieczarki
        [HttpPost]
        [Route("api/OfferedPizza/Add")]
        [ResponseType(typeof(OfferedPizza))]
        public async Task<IHttpActionResult> Add(string name, string ingredientsNames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string[] ingredientsNamesArray = ingredientsNames.Split(',');
            if (ingredientsNamesArray.Length == 0)
            {
                return BadRequest("List of ingredientsNames is empty");
            }

            name = ToTitleCase(name);
            var oldOfferedPizzas = db.OfferedPizzas.FirstOrDefault(k => k.Name == name);
            if (oldOfferedPizzas != null)
            {
                return BadRequest("Cannot insert duplicate key row. This name: " + name + " exist in db!");
            }

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Empty name field!");
            }

            List<int> ingredientsId = new List<int>();
            decimal price = basicPrice;
            foreach (var ingredientName in ingredientsNamesArray)
            {
                var titledIngredientName = ToTitleCase(ingredientName);
                var result = db.Ingredients.FirstOrDefault(k => k.Name == titledIngredientName);
                if (result == null)
                {
                    return BadRequest("Unknown ingredient name: " + titledIngredientName);
                }
                price += result.Price;
                ingredientsId.Add(result.Id_Ingredient);
            }
            
            db.OfferedPizzas.Add(new OfferedPizza() { Name = name, Price = price });

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }

            var existedPizza = db.OfferedPizzas.SingleOrDefault(r => r.Name == name);
            if(existedPizza == null)
            {
                return BadRequest("Unknown pizza name: " + name);
            }

            foreach (var id in ingredientsId)
            {
                db.IngredientsOfOfferedPizza.Add(new IngredientOfOfferedPizza()
                {
                    Id_Offered_Pizza = existedPizza.Id_Offered_Pizza,
                    Id_Ingredient = id
                });
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
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
