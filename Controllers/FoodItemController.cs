using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FoodItemsController : ControllerBase
{
    private readonly DatabaseContext _context;
    public FoodItemsController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetItems() => Ok(_context.FoodItems.ToList());

    [HttpGet("{id}")]
    public ActionResult<FoodItem> Get(int id)
    {
        var product = _context.FoodItems.Find(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public IActionResult AddItem([FromBody] FoodItem item)
    {
        _context.FoodItems.Add(item);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetItems), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, FoodItem fooditem)
    {
        var oldFoodItem = _context.FoodItems.Find(id);
        if (oldFoodItem == null) return NotFound();

        oldFoodItem.FoodItemName = fooditem.FoodItemName;
        oldFoodItem.Quantity = fooditem.Quantity;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}/reduce")]
    public IActionResult ReduceQuantity(int id, [FromQuery] int amount)
    {
        var item = _context.FoodItems.Find(id);
        if (item == null) return NotFound();

        if (item.Quantity < amount)
        {
            return BadRequest(new { Message = "Ei tarpeeksi tuotetta" });
        }

        item.Quantity -= amount;
        _context.SaveChanges();
        return Ok(new { Message = "Määrä muutettu" });
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveItem(int id)
    {
        var item = _context.FoodItems.Find(id);
        if (item == null) return NotFound();
        _context.FoodItems.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}
