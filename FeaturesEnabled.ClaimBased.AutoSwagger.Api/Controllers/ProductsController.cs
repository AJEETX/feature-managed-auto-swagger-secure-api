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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize("ShouldBeAReader")]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Delay(10);
            var product = _productService.GetById(id);

            if (product == default) return NotFound();

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [Authorize("ShouldContainRole")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Product product)
        {
            await Task.Delay(10);
            _productService.Add(product);
            return CreatedAtRoute("", new { id = product.Id }, product);
        }

        [Authorize(Roles = "Editor")]
        [Authorize("ShouldContainRole")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, Product product)
        {
            await Task.Delay(10);
            var actualProduct = _productService.GetById(id);

            if (actualProduct == default) return NotFound();

            _productService.Update(actualProduct);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Delay(10);
            var product = _productService.GetById(id);

            if (product == default) return NotFound();

            _productService.Delete(id);

            return NoContent();
        }
    }
}