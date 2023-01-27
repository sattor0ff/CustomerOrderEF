using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ProductService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<ProductDto>>> GetProducts()
    {
            var result = await _context.Products.ToListAsync();
            return new Response<List<ProductDto>>(_mapper.Map<List<ProductDto>>(result));

    }

    public async Task<Response<ProductDto>> GetProductById(int id)
    {
        try
        {
            var result = await _context.Products.FindAsync(id);
            var mapped = _mapper.Map<ProductDto>(result);
            return new Response<ProductDto>(mapped);
        }
        catch (System.Exception ex)
        {
            return new Response<ProductDto>(HttpStatusCode.InternalServerError, new List<string>() { "Server Error!" });
        }

    }
    public async Task<Response<ProductDto>> AddProduct(ProductDto product)
    {
        try
        {
            var added = _mapper.Map<Product>(product);
            _context.Products.Add(added);
            await _context.SaveChangesAsync();
            product.Id = added.Id;
            return new Response<ProductDto>(product);
        }
        catch (System.Exception ex)
        {
            return new Response<ProductDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
    public async Task<Response<ProductDto>> UpdateProduct(ProductDto product)
    {
        try
        {
            var mapped = _mapper.Map<Product>(product);
            _context.Products.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<ProductDto>(product);

        }
        catch (System.Exception ex)
        {
            return new Response<ProductDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
    public async Task<Response<string>> DeleteProduct(int id)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "NotFound" });
        _context.Products.Remove(existing);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted successfully");
    }
}