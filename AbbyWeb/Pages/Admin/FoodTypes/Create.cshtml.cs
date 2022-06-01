using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes;
[BindProperties]
public class Create : PageModel
{
    private readonly ApplicationDbContext _db;

    public Create(ApplicationDbContext db)
    {
        _db = db;
    }
    public FoodType FoodType { get; set; }
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
      
        // Custom Server-side validation
        if (!ModelState.IsValid) 
            return Page();
        
        await _db.FoodType.AddAsync(FoodType);
        await _db.SaveChangesAsync();
        TempData["success"] = "FoodType created successfully!";
        return RedirectToPage("Index");
    }
}