// using Domain.Dtos;
// using Domain.Entities;
// using Infrastructure.Data;

// namespace Infrastructure.Services;

// public class CustomerService
// {
//     private readonly DataContext _context;

//     public CustomerService(DataContext context)
//     {
//         _context = context;
//     }
    
//     public List<CustomerDto> GetCustomers()
//     {
//         return _context.Customers.Select(x=>new CustomerDto(x.Id, x.FirstName, x.LastName, x.PhoneNumber, x.Email)).ToList();
//     }
    
//     public Customer GetCustomerById(int id)
//     {
//         return _context.Customers.FirstOrDefault(x => x.Id == id);
//     }
//     public void AddCustomer(CustomerDto customer)
//     {
//         try
//         {
//             var added = new Customer()
//         {
//             Id = customer.Id,
//             FirstName = customer.FirstName,
//             LastName = customer.LastName,
//             PhoneNumber = customer.PhoneNumber,
//             Email = customer.Email
//         };
//         _context.Customers.Add(added);
//         _context.SaveChanges();
//         }
//         catch (System.Exception ex)
//         {
//             System.Console.WriteLine(ex.Message);
//         }
        
//     }
//     public void UpdateCustomer(CustomerDto customer)
//     {
//         try
//         {
//             var updated = new Customer(customer.Id, customer.FirstName, customer.LastName, customer.PhoneNumber, customer.Email);
//         _context.Customers.Update(updated);
//         _context.SaveChanges();
//         }
//         catch (System.Exception ex)
//         {
//             System.Console.WriteLine(ex.Message);
//         }
        
//     }
//     public void DeleteCustomer(int id)
//     {
//         var deleted = _context.Customers.First(x => x.Id == id);
//         _context.Customers.Remove(deleted);
//         _context.SaveChanges();
//     }
// }


using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CustomerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CustomerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Response<List<CustomerDto>>> GetCustomers()
    {
            var result = await _context.Customers.ToListAsync();
            return new Response<List<CustomerDto>>(_mapper.Map<List<CustomerDto>>(result));
    }
    
    public async Task<Response<CustomerDto>> GetCustomerById(int id)
    {
        try
        {
            var result =  await _context.Customers.FindAsync(id);
            var mapped = _mapper.Map<CustomerDto>(result);
            return new Response<CustomerDto>(mapped);
        }
        catch (System.Exception ex)
        {
            return new Response<CustomerDto>(HttpStatusCode.InternalServerError, new List<string>(){"Server Error!"});
        }
        
    }
    public async Task<Response<CustomerDto>> AddCustomer(CustomerDto customer)
    {
        try
        {
            var added = _mapper.Map<Customer>(customer);
            _context.Customers.Add(added);
            await _context.SaveChangesAsync();
            customer.Id = added.Id;
            return new Response<CustomerDto>(customer);
        }
        catch (System.Exception ex)
        {
            return new Response<CustomerDto>(HttpStatusCode.InternalServerError, new List<string>(){ex.Message});
        }
        
    }
    public async Task<Response<CustomerDto>> UpdateCustomer(CustomerDto customer)
    {
        try
        {
            var mapped = _mapper.Map<Customer>(customer);
            _context.Customers.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<CustomerDto>(customer);

        }
        catch (System.Exception ex)
        {
            return new  Response<CustomerDto> (HttpStatusCode.InternalServerError, new List<string>(){ex.Message});
        }
        
    }
    public async Task<Response<string>> DeleteCustomer(int id)
    {
        var existing  = await _context.Customers.FindAsync(id);
        if(existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>(){"NotFound"});
        _context.Customers.Remove(existing);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted successfully");
    }
}