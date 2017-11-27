using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JwtTry.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;

namespace JwtTry.Services.Identity.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IJwtHandler tokenhandler;

        public AccountController(IJwtHandler tokenhandler)
        {
            this.tokenhandler = tokenhandler;
        }

        protected AccountController()
        {
        }

        [HttpGet("login")]
        public IActionResult Get()
        {
            return Ok(tokenhandler.Create(Guid.NewGuid()));
        }


        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetInfo()
        {
            return Ok(User.Identity.Name);
        }
    }
}
