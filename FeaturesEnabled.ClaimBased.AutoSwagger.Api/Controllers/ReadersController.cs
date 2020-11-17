using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        [FeatureGate(Features.Promotions)]
        [HttpGet]
        [ProducesResponseType(typeof(List<Reader>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(10);
            var token = new JwtSecurityTokenHandler().ReadToken(Request.Headers["Authorization"].ToString().Substring(7)) as JwtSecurityToken;
            var id = token.Claims.First(claim => claim.Type == "email")?.Value;
            return Ok(DataStore.Readers);
        }

        [Authorize(Roles = "Admin")]
        [Authorize("ShouldContainRole")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Reader reader)
        {
            await Task.Delay(10);
            return CreatedAtRoute("GetTodo", new { id = reader.Id }, reader);
        }

        [Authorize(Roles = "Editor")]
        [Authorize("ShouldContainRole")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(string id, Reader reader)
        {
            await Task.Delay(10);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            await Task.Delay(10);
            return NoContent();
        }
    }
}