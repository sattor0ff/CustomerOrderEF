using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Route("[controller]")]


public class OrderController:ControllerBase
{       
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
                _orderService = orderService;
        }

        [HttpGet("GetOrder")]
        public async Task<Response<List<OrderDto>>> GetOrders()
        {
                return await _orderService.GetOrders();
        }

        [HttpGet("GetOrderById")]
        public async Task<Response<OrderDto>> GetOrderById(int id)
        {
                return await _orderService.GetOrderById(id);
        }

        [HttpPost("AddOrder")]
        public async Task<Response<OrderDto>> AddOrder(OrderDto Order)
        {
                if (ModelState.IsValid)
                {
                        return await _orderService.AddOrder(Order);
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                    return new Response<OrderDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpPut("UpdateOrder")]
        public async Task<Response<OrderDto>> UpdateOrder(OrderDto Order)
        {
                if (ModelState.IsValid)
                {
                        return await _orderService.UpdateOrder(Order);
                }
                else
                {
                        var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                        return new Response<OrderDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpDelete("DeleteOrder")]
        public async Task DeleteOrder(int id)
        {
                await _orderService.DeleteOrder(id);
        }
}       