using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes;

[BindProperties]
public class Edit : PageModel
{
    private readonly ApplicationDbContext _db;

    public Edit(ApplicationDbContext db)
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
      
        // Server-side validation
        if (!ModelState.IsValid) 
            return Page();
        
        _db.FoodType.Update(FoodType);
        await _db.SaveChangesAsync();
        TempData["success"] = "FoodType updated successfully!";
        return RedirectToPage("Index");
    }
}