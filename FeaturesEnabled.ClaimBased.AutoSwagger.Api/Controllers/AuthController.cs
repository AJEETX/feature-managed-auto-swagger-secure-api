using Microsoft.AspNetCore.Mvc;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Services;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Testing GET... all good.");
        }

        [HttpPost]
        public IActionResult Validate(LoginModel model)
        {
            return Ok(_authorizeService.Authenticate(model));
        }
    }
}