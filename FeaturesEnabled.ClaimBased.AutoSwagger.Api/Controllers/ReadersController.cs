using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        // The Roles expects a comma separated list of Role values
        [Authorize("ShouldBeAReader")]
        [HttpGet]
        public IActionResult Get()
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "email")?.Value;
            return Ok(ReaderStore.Readers);
        }

        [Authorize(Roles = "Admin")]
        [Authorize("ShouldContainRole")]
        [HttpPost]
        public List<Reader> Add()
        {
            var token = Request.Headers["Authorization"];
            return ReaderStore.Readers;
        }

        [Authorize(Roles = "Editor")]
        [Authorize("ShouldContainRole")]
        [HttpPut]
        public List<Reader> Update()
        {
            var token = Request.Headers["Authorization"];
            return ReaderStore.Readers;
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public List<Reader> Delete()
        {
            var token = Request.Headers["Authorization"];
            return ReaderStore.Readers;
        }
    }
}