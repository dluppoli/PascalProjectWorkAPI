using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWorkApi;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id {get; set;}
    [MaxLength(50)]
	public required string Name {get; set;}
    [MaxLength(255)]
	public required string Image {get; set;}
}