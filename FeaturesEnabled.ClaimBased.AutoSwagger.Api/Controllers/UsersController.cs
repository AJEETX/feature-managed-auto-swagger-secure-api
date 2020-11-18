using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize("ShouldBeAReader")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            return Ok(_userService.Users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize("ShouldBeAReader")]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Delay(10);
            if (id <= 0) return BadRequest();

            var user = _userService.GetById(id);

            if (user == default) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize("ShouldContainRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(User user)
        {
            if (user == default || !ModelState.IsValid) return BadRequest();

            await Task.Delay(10);
            _userService.Add(user);

            return CreatedAtRoute("", new { id = user.Id }, user);
        }

        [HttpPut]
        [Authorize(Roles = "Editor")]
        [Authorize("ShouldContainRole")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, User user)
        {
            var actualUser = _userService.GetById(id);

            if (actualUser == default) return NotFound();

            await Task.Delay(10);
            _userService.Update(user);

            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _userService.GetById(id);

            if (user == default) return NotFound();

            await Task.Delay(10);
            _userService.Delete(id);

            return NoContent();
        }
    }
}