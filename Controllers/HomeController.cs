using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.AllDishes = _context.Dishes.OrderByDescending(x => x.CreatedAt).ToList();
        return View();
    }

    [HttpPost("dish/add")]
    public IActionResult AddDish(Dish newDish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else {
            return View("Index");
        }
    }
    [HttpGet("edit/{dishId}")]
    public IActionResult EditDish(int dishId)
    {
        Dish dishToEdit = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
        return View(dishToEdit);
    }
    [HttpPost("dish/edit/{dishId}")]
    public IActionResult EditDish(int dishId, Dish newVerisionOfDish)
    {
        Dish oldDish = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
        oldDish.Name = newVerisionOfDish.Name;
        oldDish.ChefName = newVerisionOfDish.ChefName;
        oldDish.Calories = newVerisionOfDish.Calories;
        oldDish.Tastiness = newVerisionOfDish.Tastiness;
        oldDish.Description = newVerisionOfDish.Description;
        oldDish.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpGet("createDish")]
    public IActionResult CreateDish()
    {
        return View();
    }
    [HttpGet("/delete/{dishId}")]
    public IActionResult DeleteDish(int dishId)
    {
        Dish dishToDelete = _context.Dishes.SingleOrDefault(a => a.DishId == dishId);
        _context.Dishes.Remove(dishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("{dishId}")]
    public IActionResult ShowOne(int dishId)
    {
        ViewBag.dishToShow = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
