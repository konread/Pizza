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

namespace WebService.Controllers
{
    public class DataController : ApiController
    {
        private PizzaDbContext db = new PizzaDbContext();
        private decimal basicPrice = 15.0m;

        [HttpPost]
        [Route("api/Data/LoadIngredients")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> LoadIngredients()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Ingredient> ingredients = new List<Ingredient>() {
                new Ingredient{Name="Bazylia",Price=0.5m },new Ingredient{Name="Pomidory",Price=2.5m },
                new Ingredient{Name="Parmezan",Price=6.0m },new Ingredient{Name="Rukola",Price=1.5m },
                new Ingredient{Name="Papryka",Price=2.5m },new Ingredient{Name="Cukinia",Price=2.5m },
                new Ingredient{Name="Czosnek",Price=1.5m },new Ingredient{Name="Oregano",Price=0.5m },
                new Ingredient{Name="Ser",Price=2.1m },new Ingredient{Name="Sos",Price=1.6m },
                new Ingredient{Name="Szynka",Price=3.6m },new Ingredient{Name="Salami",Price=3.6m },
                new Ingredient{Name="Kabanosy",Price=3.6m },new Ingredient{Name="Bekon",Price=3.6m },
                new Ingredient{Name="Cebula",Price=1.7m },new Ingredient{Name="Kukurydza",Price=1.8m },
                new Ingredient{Name="Łosoś",Price=12.8m },new Ingredient{Name="Oliwki",Price=3.4m },
                new Ingredient{Name="Kapary",Price=2.9m },new Ingredient{Name="Pieczarki",Price=2.6m },
                new Ingredient{Name="Tabasco",Price=4.8m }
            };

            foreach(var ingr in ingredients)
            {
                var result = db.Ingredients.SingleOrDefault(k => k.Name == ingr.Name);
                if (result != null)
                {
                    continue;
                }
                db.Ingredients.Add(ingr);
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

        [HttpPost]
        [Route("api/Data/LoadPizzas")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> LoadPizzas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dictionary<string,List<string>> PizzaWithIngredients = new Dictionary<string, List<string>>() {
                {"Italiana",new List<string>(){"Bazylia","Pomidory","Parmezan","Rukola"}},
                {"Verdure",new List<string>(){"Papryka","Cukinia","Parmezan","Czosnek"}},
                {"Margherita",new List<string>(){"Sos","Ser","Oregano"}},
                {"Quatro",new List<string>(){"Szynka","Salami","Kabanosy","Bekon","Ser"}},
                {"Piacere",new List<string>(){"Cebula","Salami","Kukurydza","Bekon","Ser"}},
                {"Salmone",new List<string>(){"Łosoś","Oliwki","Kapary","Ser"}},
                {"Popolare",new List<string>(){"Cebula","Pieczarki","Czosnek","Bekon","Pomidory","Ser"}},
                {"Mafioso",new List<string>(){"Tabasco","Salami","Papryka","Czosnek","Ser"}}
            };

            foreach (var pwingr in PizzaWithIngredients)
            {
                var result = db.OfferedPizzas.SingleOrDefault(k => k.Name == pwingr.Key);
                if (result != null)
                {
                    continue;
                }
                db.OfferedPizzas.Add(new OfferedPizza() { Name= pwingr.Key });
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

            foreach (var pwingr in PizzaWithIngredients)
            {
                decimal price = basicPrice;
                var existedPizza = db.OfferedPizzas.SingleOrDefault(r => r.Name == pwingr.Key);
                foreach (var ingr in pwingr.Value)
                {
                    var existedIngredient = db.Ingredients.SingleOrDefault(r => r.Name == ingr);
                    if (existedIngredient == null)
                    {
                        return BadRequest("Ingredient " + ingr + " doesn't exist in database!");
                    }

                    price += existedIngredient.Price;
                    var existedIngr = db.IngredientsOfOfferedPizza.SingleOrDefault( r => 
                        r.Id_Ingredient == existedIngredient.Id_Ingredient 
                        && r.Id_Offered_Pizza == existedPizza.Id_Offered_Pizza
                    );

                    if (existedIngr == null)
                    {
                        db.IngredientsOfOfferedPizza.Add(new IngredientOfOfferedPizza()
                        {
                            Id_Offered_Pizza = existedPizza.Id_Offered_Pizza,
                            Id_Ingredient = existedIngredient.Id_Ingredient
                        });
                    }
                }
                existedPizza.Price = price;
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
    }
}