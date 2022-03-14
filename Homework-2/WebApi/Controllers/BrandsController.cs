using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add(Brand brand)
        {
            return Ok("Added");
            
        }

        [HttpGet("getbyid")]
        public Brand Get(int id)
        {
            return new Brand { Name = "Mercedes" };
        }
       
        [HttpGet("getall")]
        public List<Brand> GetAll()
        {
            return new List<Brand>()
            {
                new Brand{ Name = "Audi" },
                new Brand{ Name = "Ford" }
            };
        }
    }
}
