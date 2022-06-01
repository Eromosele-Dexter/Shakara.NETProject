using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;
[BindProperties]
public class Create : PageModel
{
    private readonly ApplicationDbContext _db;

    public Create(ApplicationDbContext db)
    {
        _db = db;
    }
    public Category Category { get; set; }
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        //Custom Server Validation
        if (Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError(Category.Name, "The Display Order and Name cannot match");
        }
        
        // Server-side validation
        if (!ModelState.IsValid) 
            return Page();
        
        await _db.Category.AddAsync(Category);
        await _db.SaveChangesAsync();
        TempData["success"] = "Category created successfully!";
        return RedirectToPage("Index");
    }
}