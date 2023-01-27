using System.ComponentModel.DataAnnotations;
using Domain.Dtos;

namespace Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string ProductName { get; set; }
    public int SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    public Product()
    {
        
    }

    public Product(int id, string productName, int supplierid, DateTime orderDate, int totalAmount)
    {
        Id = id;
        ProductName = productName;
        SupplierId = supplierid;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }
}