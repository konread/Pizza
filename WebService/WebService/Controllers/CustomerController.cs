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
    public class CustomerController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        // api/Customer/GetAll
        [HttpGet]
        [Route("api/Customer/GetAll")]
        public IQueryable<Customer> GetAll()
        {
            return db.Customers;
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

        // api/Customer/Add
        [HttpPost]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> AddObj(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldCustomer = db.Customers.Find(customer.Id_Customer);
            if (oldCustomer != null)
            {
                return BadRequest("Cannot insert duplicate key row.");
            }

            oldCustomer = db.Customers.Where(k =>
                                                k.First_Name == customer.First_Name
                                                && k.Surname == customer.Surname
                                                && k.Postal_code == customer.Postal_code).FirstOrDefault();
            if (oldCustomer != null)
            {
                return BadRequest("Customer exists in db.");
            }

            if (string.IsNullOrEmpty(customer.First_Name))
            {
                return BadRequest("Missing customer.First_Name field in object!");
            }

            if (string.IsNullOrEmpty(customer.Surname))
            {
                return BadRequest("Missing customer.Surname field in object!");
            }

            if (string.IsNullOrEmpty(customer.Street_Name))
            {
                return BadRequest("Missing customer.Street_Name field in object!");
            }

            if (string.IsNullOrEmpty(customer.City_Name))
            {
                return BadRequest("Missing customer.City_Name field in object!");
            }

            if (string.IsNullOrEmpty(customer.Postal_code) || !customer.Postal_code.Contains("-"))
            {
                return BadRequest("Missing or invalid customer.Postal_code field in object!");
            }

            if (customer.House_Number <= 0)
            {
                return BadRequest("Missing or invalid customer.House_Number field in object!");
            }

            db.Customers.Add(customer);

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

        // api/Customer/Add?name=xx&surname=xx&street=xx&houseNumber=xx&city=xx&postalCode=xx
        [HttpPost]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> Add(string name, string surname, string streetName, int houseNumber, string cityName, string postalCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldCustomer = db.Customers.Where(k =>
                                                k.First_Name == name
                                                && k.Surname == surname
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
    }
}
