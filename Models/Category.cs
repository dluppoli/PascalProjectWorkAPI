using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWorkApi;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id {get; set;}
    [MaxLength(50)]
	public string Name {get; set;}
    [MaxLength(255)]
	public string Image {get; set;}
}