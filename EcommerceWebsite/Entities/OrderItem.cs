using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebsite.Entities;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }
}