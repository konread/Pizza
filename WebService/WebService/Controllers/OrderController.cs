using System.Data.Entity.Infrastructure;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Context;
using WebService.Models;
using System;
using Newtonsoft.Json.Linq;

namespace WebService.Controllers
{
    public class OrderController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        // api/Order/GetAll
        [HttpGet]
        [Route("api/Order/GetAll")]
        public IHttpActionResult GetAll()
        {
            var orders = db.Orders;
            JObject jobject = new JObject();
            jobject.Add("Orders", JArray.FromObject(orders));
            return Ok(jobject);
        }

        [HttpGet]
        [Route("api/Order/GetAllWithPizzasAndIngredients")]
        public IHttpActionResult GetAllWithPizzasAndIngredients()
        {
            var orders = db.Orders;
            if (orders == null)
            {
                return BadRequest("Table of orders is empty!");
            }

            JArray jArray  = new JArray();
            foreach (var order in orders)
            {
                JObject tmp_obj = new JObject();
                tmp_obj.Add("Id_Order", order.Id_Order);
                tmp_obj.Add("Id_Customer", order.Id_Customer);
                tmp_obj.Add("Order_Date", order.Order_Date);
                tmp_obj.Add("Price", order.Price);
                tmp_obj.Add("Status", order.Status);

                var orderedPizzas = db.OrderedPizzas.Where(r => r.Id_Order == order.Id_Order);
                if (orderedPizzas == null)
                {
                    break;
                }

                JArray jorderedPizzas = new JArray();
                foreach(var orderedPizza in orderedPizzas)
                {
                    JObject jpizza = new JObject();
                    jpizza.Add("Id_Order", orderedPizza.Id_Order);
                    jpizza.Add("Price", orderedPizza.Price);

                    JArray jingredients = new JArray();
                    var ingredients = db.IngredientsOfOrderedPizza.Where(r => r.Id_Ordered_Pizza == orderedPizza.Id_Ordered_Pizza);
                    foreach(var ingredient in ingredients)
                    {
                        var ingr = db.Ingredients.Find(ingredient.Id_Ingredient);
                        if (ingr == null)
                        {
                            continue;
                        }

                        JObject jingredient = new JObject();
                        jingredient.Add("Name",ingr.Name);
                        jingredient.Add("Price", ingr.Price);

                        jingredients.Add(jingredient);
                    }

                    jpizza.Add("IngredientsOfOrderedPizza", jingredients);
                    jorderedPizzas.Add(jpizza);
                }

                tmp_obj.Add("orderedPizzas", jorderedPizzas);
                jArray.Add(tmp_obj);
            }
            JObject jobject = new JObject();
            jobject.Add("Orders", jArray);
            return Ok(jobject);
        }

        //api/Order/Get/1
        [HttpGet]
        [Route("api/Order/Get/{id}")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult Get(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return BadRequest("Unknown id.");
            }

            return Ok(order);
        }

        // api/Order/Delete/1
        [HttpDelete]
        [Route("api/Order/Delete/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return BadRequest("Unknown id.");
            }

            db.Orders.Remove(order);

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

        // api/Order/Add?customerId=1&price=12&date=01-01-2000&status=Z
        [HttpPost]
        [Route("api/Order/Add")]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> Add(int customerId, decimal price, DateTime date, string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            status = status.ToUpper();

            var customer = db.Customers.Find(customerId);
            if (customer == null)
            {
                return BadRequest("Customer with this id: " + customerId + " doesn't exist in db.");
            }

            if (price <= 0)
            {
                return BadRequest("Missing or invalid order.Price field in provided object!");
            }

            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Missing or invalid order.Status field in provided object!");
            }

            db.Orders.Add(new Order() {
                Id_Customer = customerId,
                Price = price,
                Order_Date = date,
                Status = status
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

        // api/Order/UpdateStatus?orderId=1&status=Z
        [HttpPost]
        [Route("api/Order/UpdateStatus")]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> UpdateStatus(int orderId, string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            status = status.ToUpper();

            var order = db.Orders.Find(orderId);
            if (order == null)
            {
                return BadRequest("Order with this id: " + orderId + " doesn't exist in db.");
            }

            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Missing or invalid order.Status field in provided object!");
            }

            order.Status = status;

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
        [Route("api/Order/AddWithPizzaAndIngredients")]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> AddWithPizzaAndIngredients(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (order.Price == 0)
            {
                return BadRequest("Empty field Price in request json");
            }

            if (order.Id_Customer == 0)
            {
                return BadRequest("Empty field Id_Customer in request json");
            }

            if (order.OrderedPizzas == null)
            {
                return BadRequest("Empty field OrderedPizzas in request json");
            }

            foreach(var pizza in order.OrderedPizzas)
            {
                if (pizza.Price == 0)
                {
                    return BadRequest("Empty or invalid field Price in pizza in request json");
                }

                if (pizza.IngredientsOfOrderedPizza == null)
                {
                    return BadRequest("Empty field IngredientsOfOrderedPizza in request json");
                }

                foreach(var ingr in pizza.IngredientsOfOrderedPizza)
                {
                    if (ingr.Id_Ingredient == 0)
                    {
                        return BadRequest("Empty field Id_Ingredient in request json");
                    }

                }
            }

            DateTime date = DateTime.UtcNow.ToLocalTime();

            db.Orders.Add(new Order() {
                Id_Customer = order.Id_Customer,
                Price = order.Price,
                Order_Date = date,
                Status = "NOWE"
            });

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }

            var orderedPizzas = order.OrderedPizzas;
            var newOrder = db.Orders.Where(r => r.Id_Customer == order.Id_Customer).ToList().LastOrDefault();
            if (newOrder == null)
            {
                return BadRequest("Order for customerid "+order.Id_Customer+" doesn't exist!");
            }

            foreach (var pizza in orderedPizzas)
            {
                db.OrderedPizzas.Add(new OrderedPizza() {
                    Id_Order = newOrder.Id_Order,
                    Price = pizza.Price
                });

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e.Message);
                }

                var IdOrderedPizza = db.OrderedPizzas.Where(r => r.Id_Order == newOrder.Id_Order).
                                        ToList().LastOrDefault().Id_Ordered_Pizza;
                foreach(var ingr in pizza.IngredientsOfOrderedPizza)
                {
                    var existedIngr = db.Ingredients.Find(ingr.Id_Ingredient);
                    if (existedIngr == null)
                    {
                        return BadRequest("Unknown ingredient id: "+ingr.Id_Ingredient);
                    }

                    db.IngredientsOfOrderedPizza.Add(new IngredientOfOrderedPizza() {
                        Id_Ordered_Pizza = IdOrderedPizza,
                        Id_Ingredient = existedIngr.Id_Ingredient
                    });
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
    }
}
