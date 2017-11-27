using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTry.Api.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(User.Identity.IsAuthenticated ? User.Identity.Name : "no one");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
