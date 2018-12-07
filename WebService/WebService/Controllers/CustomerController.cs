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
    public class CustomerController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        // api/Customer/GetAll
        [HttpGet]
        [Route("api/Customer/GetAll")]
        public IHttpActionResult GetAll()
        {
            var customers = db.Customers;
            JObject jobject = new JObject();
            jobject.Add("Customers", JArray.FromObject(customers));
            return Ok(jobject);
        }

        // api/Customer/Get/1
        [HttpGet]
        [Route("api/Customer/Get/{id}")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Get(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return BadRequest("Unknown id.");
            }

            return Ok(customer);
        }

        [HttpDelete]
        [Route("api/Customer/Delete/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return BadRequest("Unknown id.");
            }

            var ordersNumber = db.Orders.Where(k => k.Id_Customer == customer.Id_Customer).Count();
            if (ordersNumber > 0)
            {
                return BadRequest("This customer have active order/s!");
            }

            db.Customers.Remove(customer);

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

        // api/Customer/Add?name=xx&surname=xx&streetName=xx&houseNumber=xx&cityName=xx&postalCode=xx
        [HttpPost]
        [Route("api/Customer/Add")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> Add(string name, string surname, string streetName, int houseNumber, string cityName, string postalCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            name = ToTitleCase(name);
            surname = ToTitleCase(surname);
            streetName = ToTitleCase(streetName);
            cityName = ToTitleCase(cityName);

            var oldCustomer = db.Customers.Where(k =>
                                                k.First_Name == name
                                                && k.Surname == surname
                                                && k.Street_Name == streetName
                                                && k.House_Number == houseNumber
                                                && k.City_Name == cityName
                                                && k.Postal_code == postalCode).FirstOrDefault();
            if (oldCustomer != null)
            {
                return BadRequest("Customer exists in db.");
            }

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Missing customer.First_Name field in object!");
            }

            if (string.IsNullOrEmpty(surname))
            {
                return BadRequest("Missing customer.Surname field in object!");
            }

            if (string.IsNullOrEmpty(streetName))
            {
                return BadRequest("Missing customer.Street_Name field in object!");
            }

            if (string.IsNullOrEmpty(cityName))
            {
                return BadRequest("Missing customer.City_Name field in object!");
            }

            if (string.IsNullOrEmpty(postalCode) || !postalCode.Contains("-"))
            {
                return BadRequest("Missing or invalid customer.Postal_code field in object!");
            }

            if (houseNumber <= 0)
            {
                return BadRequest("Missing or invalid customer.House_Number field in object!");
            }

            db.Customers.Add(new Customer() {
                First_Name = name,
                Surname = surname,
                Street_Name = streetName,
                House_Number = houseNumber,
                City_Name = cityName,
                Postal_code = postalCode
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
