using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            this._repo = repo;            
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("{brands}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand()
        {
            var productBrand = await _repo.GetProductBrandAsync();
            return Ok(productBrand);
        }

        [HttpGet("{types}")]
        public async Task<ActionResult<ProductType>> GetProductType()
        {
            var productType = await _repo.GetProductTypesAsync();
            return Ok(productType);
        }
    }
}