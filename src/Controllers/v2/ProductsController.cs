﻿//using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain;
//using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.FeatureManagement.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers.v2
//{
//    [ApiVersion("2.0")]
//    [Route("api/v{version:apiVersion}/[controller]")]
//    [ApiController]
//    public class ProductsController : ControllerBase
//    {
//        private readonly IProductService _productService;

//        public ProductsController(IProductService productService)
//        {
//            _productService = productService;
//        }

//        [FeatureGate(Features.Promotions)]
//        [HttpGet]
//        [Authorize("ShouldContainRole")]
//        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Get()
//        {
//            await Task.Delay(10);
//            try
//            {
//                var products = _productService.Products;
//                return Ok(products);
//            }
//            catch (Exception ex)
//            {
//                //log//
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [FeatureGate(Features.UserSuggestions)]
//        [HttpGet("{id}")]
//        [Authorize("ShouldBeAReader")]
//        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            if (id <= 0) return BadRequest();
//            try
//            {
//                var product = _productService.GetById(id);

//                if (product == default) return NotFound();
//                await Task.Delay(10);

//                return Ok(product);
//            }
//            catch (Exception ex)
//            {
//                //log//
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpPost]
//        [Authorize(Roles = "Admin")]
//        [Authorize("ShouldContainRole")]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Post(Product product)
//        {
//            if (product == default || !ModelState.IsValid) return BadRequest();
//            try
//            {
//                _productService.Add(product);
//                await Task.Delay(10);

//                return CreatedAtRoute("", new { id = product.Id }, product);
//            }
//            catch (Exception ex)
//            {
//                //log//
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpPut]
//        [Authorize(Roles = "Editor")]
//        [Authorize("ShouldContainRole")]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Put(int id, Product product)
//        {
//            if (id != product.Id || !ModelState.IsValid) return BadRequest();

//            try
//            {
//                var actualProduct = _productService.GetById(id);

//                if (actualProduct == default) return NotFound();

//                await Task.Delay(10);
//                _productService.Update(product);

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                //log//
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpDelete]
//        [Authorize(Roles = "Admin")]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Delete(int id)
//        {
//            if (id <= 0) return BadRequest();
//            try
//            {
//                var product = _productService.GetById(id);

//                if (product == default) return NotFound();

//                await Task.Delay(10);
//                _productService.Delete(id);

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                //log//
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }
//    }
//}