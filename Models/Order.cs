using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWorkApi;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string Address { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    public double TotalPrice { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail>? Details { get; set; } = null;
}
