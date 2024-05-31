﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWorkApi;

public class Product
{
    [Key]
	public int Id {get; set;}

    [MaxLength(100)]
	public string Title {get; set;}

    [MaxLength(1024)]
	public string Description {get; set;}

	public double Price {get; set;}
	public double Stars {get; set;}
    [MaxLength(1024)]
	public string Images {get; set;}
	public int IdCategory {get; set;}

    [ForeignKey("IdCategory")]
    //[InverseProperty("Categories")]
    public Category Category { get; set; } = null!;
}
