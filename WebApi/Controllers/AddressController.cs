using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Route("[controller]")]


public class AddressController:ControllerBase
{       
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
                _addressService = addressService;
        }

        [HttpGet("GetAddress")]
        public async Task<Response<List<AddressDto>>> GetAddresses()
        {
                return await _addressService.GetAddresses();
        }

        [HttpGet("GetAddressById")]
        public async Task<Response<AddressDto>> GetAddressById(int id)
        {
                return await _addressService.GetAddressById(id);
        }

        [HttpPost("AddAddress")]
        public async Task<Response<AddressDto>> AddAddress(AddressDto address)
        {
                if (ModelState.IsValid)
                {
                        return await _addressService.AddAddress(address);
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                    return new Response<AddressDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpPut("UpdateAddress")]
        public async Task<Response<AddressDto>> UpdateAddress(AddressDto address)
        {
                if (ModelState.IsValid)
                {
                        return await _addressService.UpdateAddress(address);
                }
                else
                {
                        var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                        return new Response<AddressDto>(HttpStatusCode.BadRequest, errors);
                }
        }

        [HttpDelete("DeleteAddress")]
        public async Task DeleteAddress(int id)
        {
                await _addressService.DeleteAddress(id);
        }
}       