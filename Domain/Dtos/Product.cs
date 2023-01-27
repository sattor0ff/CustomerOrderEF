namespace Domain.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalAmount { get; set; }

    public ProductDto()
    {
        
    }

    public ProductDto(int id, string productName, int supplierid, DateTime orderDate, int totalAmount)
    {
        Id = id;
        ProductName = productName;
        SupplierId = supplierid;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }
}