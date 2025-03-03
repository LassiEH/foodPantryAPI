using System.ComponentModel.DataAnnotations;

public class FoodItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FoodItemName { get; set; }
    public int Quantity { get; set; }
    
}