using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthorizeController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Validate(LoginModel model)
        {
            if (model == default || !ModelState.IsValid) return BadRequest();

            return Ok(_authorizeService.Authenticate(model));
        }
    }
}