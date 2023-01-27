using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Dtos;

public class Order
{
    [Key]
    public int Id { get; set; }
    public string OrderNumber  { get; set; }
    [Required, MaxLength(50)]
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public Customer Customer { get; set; }

    public Order()
    {
        
    }

    public Order(int id, string ordernumber, int customerid, DateTime orderDate, decimal totalAmount)
    {
        Id = id;
        OrderNumber = ordernumber;
        CustomerId = customerid;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }
}   