namespace Domain.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber  { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public OrderDto()
    {
        
    }

    public OrderDto(int id, string ordernumber, int customerid, DateTime orderDate, decimal totalAmount)
    {
        Id = id;
        OrderNumber = ordernumber;
        CustomerId = customerid;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }
}