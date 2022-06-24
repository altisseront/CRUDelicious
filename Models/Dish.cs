#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public string ChefName {get;set;}
    [Required]
    [Range(1, 9999999)]
    public int Calories {get;set;}
    [Required]
    [Range(1, 5)]
    public int Tastiness {get;set;}
    [Required]
    public string Description {get;set;}
    [Required]
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    [Required]
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}