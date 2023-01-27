namespace Domain.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public int CityId { get; set; }
    public int PostalCode { get; set; }
    public int CustomerId { get; set; }

    public AddressDto()
    {
        
    }

    public AddressDto(int id, string address1, string address2, int cityid, int postalcode, int customerid)
    {
        Id = id;
        Address1 = address1;
        Address2 = address2;
        CityId = cityid;
        PostalCode = postalcode;
        CustomerId = customerid;
    }
    
}