
namespace Domain.Dtos;

public class OrderItemDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    
    
    public OrderItemDto()
    {
        
    }

    public OrderItemDto(int orderid, int productid, decimal unitprice, int quantity)
    {
        OrderId = orderid;
        ProductId = productid;
        UnitPrice = unitprice;
        Quantity = quantity;
    }
}