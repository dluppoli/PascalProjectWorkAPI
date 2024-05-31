using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWorkApi;

[Table("OrderDetail")]
public class OrderDetail
{
    [Key]
    public int Id { get; set; }
    public int IdOrder { get; set; }
    public int IdProduct { get; set; }
    public int Quantity { get; set; }

    [ForeignKey("IdOrder")]
    public Order Order { get; set; }

    [ForeignKey("IdProduct")]
    public Product Product { get; set; }
}
