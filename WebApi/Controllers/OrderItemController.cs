using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Route("[controller]")]


public class OrderItemController:ControllerBase
{       
        private readonly OrderItemService _orderItemService;

        public OrderItemController(OrderItemService orderItemService)
        {
                _orderItemService = orderItemService;
        }

        [HttpGet("GetOrderItem")]
        public async Task<Response<List<OrderItemDto>>> GetOrderItems()
        {
                return await _orderItemService.GetOrderItems();
        }

        [HttpGet("GetOrderItemById")]
        public async Task<Response<OrderItemDto>> GetOrderItemById(int id)
        {
                return await _orderItemService.GetOrderItemById(id);
        }

        [HttpPost("AddOrderItem")]
        public async Task<Response<OrderItemDto>> AddOrderItem(OrderItemDto OrderItem)
        {
                if (ModelState.IsValid)
                {
                        return await _orderItemService.AddOrderItem(OrderItem);
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                    return new Response<OrderItemDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpPut("UpdateOrderItem")]
        public async Task<Response<OrderItemDto>> UpdateOrderItem(OrderItemDto OrderItem)
        {
                if (ModelState.IsValid)
                {
                        return await _orderItemService.UpdateOrderItem(OrderItem);
                }
                else
                {
                        var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                        return new Response<OrderItemDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpDelete("DeleteOrderItem")]
        public async Task DeleteOrderItem(int id)
        {
                await _orderItemService.DeleteOrderItem(id);
        }
}       