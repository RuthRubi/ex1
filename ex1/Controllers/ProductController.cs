using ex1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace ex1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        static Products p1 = new Products { Name = "milk", Id = "123", Price = 5 , Category = "a" };
        static Products p2 = new Products { Name = "candies", Id = "432", Price = 21, Category = "b" };
        static Products p3 = new Products { Name = "coffee", Id = "678", Price = 12 , Category ="c"};

        static List<Products> ProductsList = new List<Products>() { p1, p2, p3 };


        [HttpGet("GetAllProducts")]
        public ActionResult<List<Products>> GetAllProducts()
        {
            return Ok(ProductsList);
        }

        [HttpGet("GetProductById{id}")]
        public ActionResult<Products> GetProductById(string id)
        {
            Products p = ProductsList.Find(item => item.Id == id);
            return Ok(p);
        }


        [HttpPost("AddProducts")]
        public void CreateProduct([FromBody] Products Prod)
        {
            ProductsList.Add(Prod);
        }

        [HttpPut("UpdateProducts{id}")]
        public void Update([FromBody] Products Prod, string id)
        {
            Products p = ProductsList.Find(item => item.Id == id);
            if (p != null)
            {
            if(Prod.Name!="string") p.Name = Prod.Name;
            if (Prod.Price != 0) p.Price = Prod.Price;
            if (Prod.Category != "string") p.Category = Prod.Category;
            }

        }
        [HttpDelete("DeleteProduct{id}")]
        public void Delete(string id)
        {
            ProductsList.Remove(ProductsList.Find(item => item.Id == id));

        }
        [HttpGet("SearchByName{name}")]
        public ActionResult<Products> SearchByName(string name)
        {
           List< Products> p = ProductsList.FindAll(item => item.Name == name);
            return Ok(p);
        }
        [HttpGet("category{category}")]
        public ActionResult<Products> SearchByCategory(string category)
        {
            List<Products> p = ProductsList.FindAll(item => item.Category == category);
            return Ok(p);
        }

        [HttpGet("range")]
        public ActionResult<Products> GetByRange(int minPrice, int maxPrice)
        {
            List<Products> p = ProductsList.FindAll(item => item.Price > minPrice && item.Price < maxPrice);
            return Ok(p);
        }
    }
}
