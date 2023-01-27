using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AddressService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AddressService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Response<List<AddressDto>>> GetAddresses()
    {
            var result = await _context.Addresses.ToListAsync();
            return new Response<List<AddressDto>>(_mapper.Map<List<AddressDto>>(result));  
    }
    
    public async Task<Response<AddressDto>> GetAddressById(int id)
    {
            var result =  await _context.Addresses.FindAsync(id);
            var mapped = _mapper.Map<AddressDto>(result);
            return new Response<AddressDto>(mapped);
        
    }
    public async Task<Response<AddressDto>> AddAddress(AddressDto address)
    {
        try
        {
            var added = _mapper.Map<Address>(address);
            _context.Addresses.Add(added);
            await _context.SaveChangesAsync();
            address.Id = added.Id;
            return new Response<AddressDto>(address);
        }
        catch (System.Exception ex)
        {
            return new Response<AddressDto>(HttpStatusCode.InternalServerError, new List<string>(){ex.Message});
        }
        
    }
    public async Task<Response<AddressDto>> UpdateAddress(AddressDto address)
    {
        try
        {
            var mapped = _mapper.Map<Address>(address);
            _context.Addresses.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddressDto>(address);

        }
        catch (System.Exception ex)
        {
            return new  Response<AddressDto> (HttpStatusCode.InternalServerError, new List<string>(){ex.Message});
        }
        
    }
    public async Task<Response<string>> DeleteAddress(int id)
    {
        var existing  = await _context.Addresses.FindAsync(id);
        if(existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>(){"NotFound"});
        _context.Addresses.Remove(existing);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted successfully");
    }
}