using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FoodItemsController : ControllerBase
{
    private static List<FoodItem> PantryItems = new List<FoodItem>
    {
        new FoodItem { Id = 1, FoodItemName = "Jauho", Quantity = 1 },
        new FoodItem { Id = 2, FoodItemName = "Leivinjauhe", Quantity = 1 }
    };

    [HttpGet]
    public IEnumerable<FoodItem> Get() => PantryItems;

    [HttpGet("{id}")]
    public ActionResult<FoodItem> Get(int id)
    {
        var product = PantryItems.Find(p => p.Id == id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public ActionResult<FoodItem> Post(FoodItem fooditem)
    {
        fooditem.Id = PantryItems.Count + 1;
        PantryItems.Add(fooditem);
        return CreatedAtAction(nameof(Get), new { id = fooditem.Id }, fooditem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, FoodItem fooditem)
    {
        var oldFoodItem = PantryItems.Find(p => p.Id == id);
        if (oldFoodItem == null) return NotFound();

        oldFoodItem.FoodItemName = fooditem.FoodItemName;
        oldFoodItem.Quantity = fooditem.Quantity;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var foodItem = PantryItems.Find(p => p.Id == id);
        if (foodItem == null) return NotFound();

        PantryItems.Remove(foodItem);
        return NoContent();
    }
}
