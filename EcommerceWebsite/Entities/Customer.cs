using System.ComponentModel.DataAnnotations;

namespace EcommerceWebsite.Entities;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }


}