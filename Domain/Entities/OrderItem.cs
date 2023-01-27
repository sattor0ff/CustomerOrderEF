using Domain.Dtos;

namespace Domain.Entities;

public class OrderItem
{
    
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
    
    public OrderItem()
    {
        
    }

    public OrderItem(int orderid, int productid, decimal unitprice, int quantity)
    {
        OrderId = orderid;
        ProductId = productid;
        UnitPrice = unitprice;
        Quantity = quantity;
    
    }
}