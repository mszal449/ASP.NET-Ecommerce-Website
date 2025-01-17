using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EcommerceWebsite.Entities;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [Required]
    [ForeignKey("Customer")]
    public string? UserId { get; set; }
    public User User { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}