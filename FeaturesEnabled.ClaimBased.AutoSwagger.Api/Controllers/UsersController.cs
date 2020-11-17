using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;

        public UsersController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            return Ok("Testing GET... all good.");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Validate(LoginModel model)
        {
            return Ok(_authorizeService.Authenticate(model));
        }
    }
}