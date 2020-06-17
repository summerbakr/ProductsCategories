using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
{
public class Product{
[Key]
public int ProductId{get; set;}

[Required]
[MinLength(2)]
public string Name{get; set;}
[Required]
[MinLength(10)]
public string Description{get;set;}
[Required]
[Range(0, 1000000)]
public double Price{get; set;}

public DateTime CreatedAt{get;set;}=DateTime.Now;

public DateTime UpdatedAt{get;set;}=DateTime.Now;

public List<Association> ProductCategories {get;set;}

}
}
