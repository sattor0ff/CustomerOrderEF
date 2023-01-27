using System.ComponentModel.DataAnnotations;
using Domain.Dtos;

namespace Domain.Entities;

public class Customer
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<Order> Orders { get; set; }

    public Customer()
    {
        
    }

    public Customer(int id, string firstname, string lastname, string phonenumber, string email)
    {
        Id = id;
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        Email = email;
    }
}
