
namespace Domain.Dtos;

public class CustomerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public CustomerDto()
    {
        
    }

    public CustomerDto(int id, string firstname, string lastname, string phonenumber, string email)
    {
        Id = id;
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        Email = email;
    }
}
