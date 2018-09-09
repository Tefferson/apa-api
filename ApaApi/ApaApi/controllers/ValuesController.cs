using System;
using Microsoft.AspNetCore.Mvc;

namespace ApaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Console.WriteLine(value);
        }
    }
}
