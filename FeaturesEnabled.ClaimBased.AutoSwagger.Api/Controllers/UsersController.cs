using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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
        [Authorize("ShouldContainRole")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var users = _userService.Users;
                return Ok(users);
            }
            catch (Exception ex)
            {
                //log//
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize("ShouldBeAReader")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                var user = _userService.GetById(id);

                if (user == default) return NotFound();
                await Task.Delay(10);

                return Ok(user);
            }
            catch (Exception ex)
            {
                //log//
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize("ShouldBeAnAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(User user)
        {
            if (user == default || !ModelState.IsValid) return BadRequest();
            try
            {
                _userService.Add(user);
                await Task.Delay(10);

                return CreatedAtRoute("", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                //log//
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Editor")]
        [Authorize("ShouldContainRole")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, User user)
        {
            if (id != user.Id || !ModelState.IsValid) return BadRequest();
            try
            {
                var actualUser = _userService.GetById(id);

                if (actualUser == default) return NotFound();

                await Task.Delay(10);
                _userService.Update(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                //log//
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                var user = _userService.GetById(id);

                if (user == default) return NotFound();

                await Task.Delay(10);
                _userService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                //log//
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}