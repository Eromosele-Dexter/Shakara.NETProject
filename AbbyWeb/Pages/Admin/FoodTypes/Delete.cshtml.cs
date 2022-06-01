
using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes;

[BindProperties]
public class Delete : PageModel
{
    private readonly ApplicationDbContext _db;

    public Delete(ApplicationDbContext db)
    {
        _db = db;
    }
    public FoodType FoodType { get; set; }
    
    public void OnGet(int id)
    {
        FoodType = _db.FoodType.Find(id);
    }
    
    public async Task<IActionResult> OnPost()
    {
        var foodTypeFromDb = _db.FoodType.Find(FoodType.Id);

        if (foodTypeFromDb != null)
        {
            _db.FoodType.Remove(foodTypeFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "FoodType deleted successfully!";
            return RedirectToPage("Index");
        }

        return Page();
    }
}