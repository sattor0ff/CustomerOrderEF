using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public OrderService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<OrderDto>>> GetOrders()
    {
            var result = await _context.Orders.ToListAsync();
            return new Response<List<OrderDto>> (_mapper.Map<List<OrderDto>>(result));

    }

    public async Task<Response<OrderDto>> GetOrderById(int id)
    {
        try
        {
            var result = await _context.Orders.FindAsync(id);
            var mapped = _mapper.Map<OrderDto>(result);
            return new Response<OrderDto>(mapped);
        }
        catch (System.Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError, new List<string>() { "Server Error!" });
        }

    }
    public async Task<Response<OrderDto>> AddOrder(OrderDto order)
    {
        try
        {
            var added = _mapper.Map<Order>(order);
            _context.Orders.Add(added);
            await _context.SaveChangesAsync();
            order.Id = added.Id;
            return new Response<OrderDto>(order);
        }
        catch (System.Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
    public async Task<Response<OrderDto>> UpdateOrder(OrderDto order)
    {
        try
        {
            var mapped = _mapper.Map<Order>(order);
            _context.Orders.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<OrderDto>(order);

        }
        catch (System.Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
    public async Task<Response<string>> DeleteOrder(int id)
    {
        var existing = await _context.Orders.FindAsync(id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "NotFound" });
        _context.Orders.Remove(existing);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted successfully");
    }
}