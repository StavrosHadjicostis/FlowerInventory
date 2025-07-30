using Microsoft.AspNetCore.Mvc;
using FlowerInventory.Models;
using FlowerInventory.Services;

public class FlowersController : Controller
{
    private readonly IFlowerService _flowerService;
    private readonly ICategoryService _categoryService;

    public FlowersController(IFlowerService flowerService, ICategoryService categoryService)
    {
        _flowerService = flowerService;
        _categoryService = categoryService;
    }

    // GET: Flowers (Home page with search & sort)
    [HttpGet("")]
    public IActionResult Index(string searchString, string sortOrder)
    {
        var flowers = _flowerService.GetAll();

        // Search
        if (!string.IsNullOrEmpty(searchString))
        {
            flowers = flowers.Where(f => f.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Sort by Price or Name
        flowers = sortOrder switch
        {
            "price" => flowers.OrderBy(f => f.Price).ToList(),
            "name" => flowers.OrderBy(f => f.Name).ToList(),
            _ => flowers.ToList()
        };

        return View(flowers);
    }

    // GET: Details
    [HttpGet("Details")]
    public IActionResult Details(int id)
    {
        var flower = _flowerService.GetById(id);
        if (flower == null) return NotFound();
        return View(flower);
    }

    // GET: Add/Edit
    [HttpGet("Upsert/{id?}")]
    public IActionResult Upsert(int? id)
    {
        ViewBag.Categories = _categoryService.GetAll();
        if (id == null) return View(new Flower());
        var flower = _flowerService.GetById(id.Value);
        if (flower == null) return NotFound();
        return View(flower);
    }

    // POST: Add/Edit
   [HttpPost("Upsert/{id?}")]
    public IActionResult Upsert(Flower flower)
    {
        ModelState.Remove("Category"); 
        Console.WriteLine($"ModelState valid: {ModelState.IsValid}");
        foreach (var state in ModelState)
        {
            foreach (var error in state.Value.Errors)
            {
                Console.WriteLine($"{state.Key}: {error.ErrorMessage}");
            }
        }
        if (!ModelState.IsValid) {
            ViewBag.Categories = _categoryService.GetAll();
            return View(flower);
        }
        if (flower.Id == 0) _flowerService.Add(flower);
        else _flowerService.Update(flower);
        return RedirectToAction(nameof(Index));
    }

    // GET: Delete Confirmation
    public IActionResult Delete(int id)
    {
        var flower = _flowerService.GetById(id);
        if (flower == null) return NotFound();
        return View(flower);
    }

    // POST: Delete
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _flowerService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
