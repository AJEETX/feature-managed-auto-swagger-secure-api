﻿using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/authorize")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthorizeController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// user login with email address
        /// </summary>
        /// <param name="model"></param>
        /// <returns>token</returns>
        [HttpPost("verify")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Verify(LoginRequest model)
        {
            if (model == default || !ModelState.IsValid) return BadRequest("Incorrect login details");
            try
            {
                var userToken = _authorizeService.Authenticate(model);

                if (!userToken.IsSuccess) return NotFound("credential(s) incorrect");

                return Ok(userToken);
            }
            catch (Exception ex)
            {
                //log//
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}