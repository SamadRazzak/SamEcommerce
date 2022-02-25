using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using API.Dtos;
using Microsoft.AspNetCore.Http;
using API.Errors;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {        
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrand;
        private readonly IGenericRepository<ProductType> _productType;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, 
                                    IGenericRepository<ProductBrand> productBrand, 
                                    IGenericRepository<ProductType> productType,
                                    IMapper mapper)
        {            
            this._productRepo = productRepo;
            this._productBrand = productBrand;
            this._productType = productType;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {  
            var spec = new ProductWithTypesAndBrandsSpecification();

            var products = await _productRepo.ListAsync(spec);
             return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {   
            var spec = new ProductWithTypesAndBrandsSpecification(id);       

            var product = await _productRepo.GetEntityWithSpec(spec);
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("{brands}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand()
        {
            var productBrand = await _productBrand.ListAllAsync();
            return Ok(productBrand);
        }

        [HttpGet("{types}")]
        public async Task<ActionResult<ProductType>> GetProductType()
        {
            var productType = await _productType.ListAllAsync();
            return Ok(productType);
        }
    }
}