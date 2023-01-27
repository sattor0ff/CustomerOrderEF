using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class Address
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public int CityId { get; set; }
    [Required, MaxLength(50)]
    public int PostalCode { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public Address()
    {
        
    }
    
    public Address(int id, string address1, string address2, int cityid, int postalCode, int customerid)
    {
        Id = id;
        Address1 = address1;
        Address2 = address2;
        CityId = cityid;
        PostalCode = postalCode;
        CustomerId = customerid;
    }

}