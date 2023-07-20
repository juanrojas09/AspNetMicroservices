using Amazon.Runtime.Internal.Util;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepositories _productRepositories;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepositories productRepositories, ILogger<CatalogController> logger)
        {

            _productRepositories = productRepositories;
            _logger = logger;

        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepositories.GetProducts();
            return Ok(products);
        }
         

        [HttpGet("Id")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepositories.GetProduct(id);
            return Ok(product);

        }
        
        [HttpGet("GetProductByCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var product = _productRepositories.GetProductByCategory(category);
            return Ok(product);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepositories.CreateProduct(product);
            return Ok(product);

            //await _productRepositories.CreateProduct(product);
            //return CreatedAtRoute("Create", new { id = product.Id }, product;
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepositories.UpdateProduct(product));
        }


        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> DeleteProductById(string id)
        {
            var product = await _productRepositories.DeleteProduct(id);
            return Ok(product);
        }

    }
}
