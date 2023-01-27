using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderItemService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public OrderItemService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<OrderItemDto>>> GetOrderItems()
    {
            var result = await _context.OrderItems.ToListAsync();
            return new Response<List<OrderItemDto>>(_mapper.Map<List<OrderItemDto>>(result));

    }

    public async Task<Response<OrderItemDto>> GetOrderItemById(int id)
    {
        try
        {
            var result = await _context.OrderItems.FindAsync(id);
            var mapped = _mapper.Map<OrderItemDto>(result);
            return new Response<OrderItemDto>(mapped);
        }
        catch (System.Exception ex)
        {
            return new Response<OrderItemDto>(HttpStatusCode.InternalServerError, new List<string>() { "Server Error!" });
        }

    }
    public async Task<Response<OrderItemDto>> AddOrderItem(OrderItemDto orderItem)
    {
        try
        {
            var added = _mapper.Map<OrderItem>(orderItem);
            _context.OrderItems.Add(added);
            await _context.SaveChangesAsync();
            return new Response<OrderItemDto>(orderItem);
        }
        catch (System.Exception ex)
        {
            return new Response<OrderItemDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
    public async Task<Response<OrderItemDto>> UpdateOrderItem(OrderItemDto orderItem)
    {
        try
        {
            var mapped = _mapper.Map<OrderItem>(orderItem);
            _context.OrderItems.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<OrderItemDto>(orderItem);

        }
        catch (System.Exception ex)
        {
            return new Response<OrderItemDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }

    }
    public async Task<Response<string>> DeleteOrderItem(int id)
    {
        var existing = await _context.OrderItems.FindAsync(id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "NotFound" });
        _context.OrderItems.Remove(existing);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted successfully");
    }
}