using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWorkApi;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public required string ClientName { get; set; }
    public required string Address { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }
    public required double TotalPrice { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail>? Details { get; set; } = null;
}
