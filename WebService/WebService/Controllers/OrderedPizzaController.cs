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
    public class OrderedPizzaController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        // api/OrderedPizza/GetAll
        [HttpGet]
        [Route("api/OrderedPizza/GetAll")]
        public IHttpActionResult GetAll()
        {
            var orderedPizzas = db.OrderedPizzas;
            JObject jobject = new JObject();
            jobject.Add("OrderedPizzas", JArray.FromObject(orderedPizzas));
            return Ok(jobject);
        }

        // api/OrderedPizza/Get/1
        [HttpGet]
        [Route("api/OrderedPizza/Get/{id}")]
        [ResponseType(typeof(OrderedPizza))]
        public IHttpActionResult Get(int id)
        {
            OrderedPizza orderedPizza = db.OrderedPizzas.Find(id);
            if (orderedPizza == null)
            {
                return BadRequest("Unknown id.");
            }

            return Ok(orderedPizza);
        }

        // api/OrderedPizza/Add?idOrder=1&price=56.2&&ingredientsNames=sos,ser,pieczarki
        [HttpPost]
        [Route("api/OrderedPizza/Add")]
        [ResponseType(typeof(OrderedPizza))]
        public async Task<IHttpActionResult> Add(int idOrder, decimal price, string ingredientsNames)
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

            var order = db.Orders.Find(idOrder);
            if (order == null)
            {
                return BadRequest("Order: "+idOrder+" doesn't exist!");
            }

            if (price <= 0.0m)
            {
                return BadRequest("Invalid price value!");
            }

            List<int> ingredientsId = new List<int>();
            foreach (var ingredientName in ingredientsNamesArray)
            {
                var titledIngredientName = ToTitleCase(ingredientName);
                var result = db.Ingredients.FirstOrDefault(k => k.Name == titledIngredientName);
                if (result == null)
                {
                    return BadRequest("Unknown ingredient name: " + titledIngredientName);
                }
                ingredientsId.Add(result.Id_Ingredient);
            }

            db.OrderedPizzas.Add(new OrderedPizza() { Id_Order = idOrder, Price = price });

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }

            var existedPizza = db.OrderedPizzas.SingleOrDefault(r => r.Id_Order == idOrder);
            if (existedPizza == null)
            {
                return BadRequest("Unknown order id: " + idOrder);
            }

            foreach (var id in ingredientsId)
            {
                db.IngredientsOfOrderedPizza.Add(new IngredientOfOrderedPizza()
                {
                    Id_Ordered_Pizza = existedPizza.Id_Ordered_Pizza,
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

        // api/OrderedPizza/Delete/1
        [HttpDelete]
        [Route("api/OrderedPizza/Delete/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            OrderedPizza orderedPizza = await db.OrderedPizzas.FindAsync(id);
            if (orderedPizza == null)
            {
                return BadRequest("Unknown id.");
            }

            Order order = await db.Orders.FindAsync(orderedPizza.Id_Order);
            if (order != null)
            {
                return BadRequest("Cannot delete orderedPizza. Existing order with id: "+ orderedPizza.Id_Order);
            }

            db.OrderedPizzas.Remove(orderedPizza);

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
