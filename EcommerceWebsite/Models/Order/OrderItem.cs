
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebsite.Models.Order;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Entities.Order Order { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Entities.Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }
}