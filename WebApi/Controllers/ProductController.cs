using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Route("[controller]")]


public class ProductController:ControllerBase
{       
        private readonly ProductService _productService;

        public ProductController(ProductService ProductService)
        {
                _productService = ProductService;
        }

        [HttpGet("GetProduct")]
        public async Task<Response<List<ProductDto>>> GetProducts()
        {
                return await _productService.GetProducts();
        }

        [HttpGet("GetProductById")]
        public async Task<Response<ProductDto>> GetProductById(int id)
        {
                return await _productService.GetProductById(id);
        }

        [HttpPost("AddProduct")]
        public async Task<Response<ProductDto>> AddProduct(ProductDto product)
        {
                if (ModelState.IsValid)
                {
                        return await _productService.AddProduct(product);
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                    return new Response<ProductDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpPut("UpdateProduct")]
        public async Task<Response<ProductDto>> UpdateProduct(ProductDto product)
        {
                if (ModelState.IsValid)
                {
                        return await _productService.UpdateProduct(product);
                }
                else
                {
                        var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                        return new Response<ProductDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpDelete("DeleteProduct")]
        public async Task DeleteProduct(int id)
        {
                await _productService.DeleteProduct(id);
        }
}       