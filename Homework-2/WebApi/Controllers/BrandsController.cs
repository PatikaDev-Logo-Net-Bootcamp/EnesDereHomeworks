using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
   // [ApiVersion("1.0")]
   // [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    //[Route("api/v{version:apiVersion}/[controller]")]
    public class BrandsController : ControllerBase
    {
        //[HttpGet]
        //public string Get(ApiVersion apiVersion)
        //    => $"Controller = {GetType().Name} \nVersion = {apiVersion}";
       // [MapToApiVersion("1.0")]
        [HttpPost("add")]
        public IActionResult Add()
        {
            return Ok("Added");
            
        }
       // [MapToApiVersion("1.0")]
        [HttpGet("getbyid")]
        public Brand Get(int id)
        {
            return new Brand { Name = "Mercedes" };
        }
       // [MapToApiVersion("2.0")]
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
