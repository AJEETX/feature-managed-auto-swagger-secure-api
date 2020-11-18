using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [FeatureGate(Features.Promotions)]
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize("ShouldBeAReader")]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(10);
            return Ok(_productService.Products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize("ShouldBeAReader")]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Delay(10);
            if (id <= 0) return BadRequest();

            var product = _productService.GetById(id);

            if (product == default) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize("ShouldContainRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Product product)
        {
            if (product == default || !ModelState.IsValid) return BadRequest();

            await Task.Delay(10);
            _productService.Add(product);

            return CreatedAtRoute("", new { id = product.Id }, product);
        }

        [HttpPut]
        [Authorize(Roles = "Editor")]
        [Authorize("ShouldContainRole")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, Product product)
        {
            var actualProduct = _productService.GetById(id);

            if (actualProduct == default) return NotFound();

            await Task.Delay(10);
            _productService.Update(product);

            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _productService.GetById(id);

            if (product == default) return NotFound();

            await Task.Delay(10);
            _productService.Delete(id);

            return NoContent();
        }
    }
}