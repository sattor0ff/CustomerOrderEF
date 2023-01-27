using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Route("[controller]")]


public class CustomerController:ControllerBase
{       
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
                _customerService = customerService;
        }

        [HttpGet("GetCustomer")]
        public async Task<Response<List<CustomerDto>>> GetCustomers()
        {
                return await _customerService.GetCustomers();
        }

        [HttpGet("GetCustomerById")]
        public async Task<Response<CustomerDto>> GetCustomerById(int id)
        {
                return await _customerService.GetCustomerById(id);
        }

        [HttpPost("AddCustomer")]
        public async Task<Response<CustomerDto>> AddCustomer(CustomerDto customer)
        {
                if (ModelState.IsValid)
                {
                        return await _customerService.AddCustomer(customer);
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                    return new Response<CustomerDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpPut("UpdateCustomer")]
        public async Task<Response<CustomerDto>> UpdateCustomer(CustomerDto customer)
        {
                if (ModelState.IsValid)
                {
                        return await _customerService.UpdateCustomer(customer);
                }
                else
                {
                        var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                        return new Response<CustomerDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpDelete("DeleteCustomer")]
        public async Task DeleteCustomer(int id)
        {
                await _customerService.DeleteCustomer(id);
        }
}       