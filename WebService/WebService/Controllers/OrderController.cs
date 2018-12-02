﻿using System.Collections.Generic;
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
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebService.Controllers
{
    public class OrderController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();

        // api/Order/GetAll
        [HttpGet]
        [Route("api/Order/GetAll")]
        public IQueryable<Order> GetAll()
        {
            return db.Orders;
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

        // api/Order/AddObj
        [HttpPost]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> AddObj(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldOrder = db.Orders.Find(order.Id_Order);
            if (oldOrder != null)
            {
                return BadRequest("Cannot insert duplicate key row.");
            }

            var customer = db.Customers.Find(order.Id_Customer);
            if (customer == null)
            {
                return BadRequest("Customer with this id: " + order.Id_Customer + " doesn't exist in db.");
            }

            if (order.Price <= 0)
            {
                return BadRequest("Missing or invalid order.Price field in provided object!");
            }

            if (string.IsNullOrEmpty(order.Status))
            {
                return BadRequest("Missing order.Status field in provided object!");
            }

            db.Orders.Add(order);

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
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> Add(int customerId, double price, DateTime date, string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
                return BadRequest("Missing order.Status field in provided object!");
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
    }
}