using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories;

[BindProperties]
public class Edit : PageModel
{
    private readonly ApplicationDbContext _db;

    public Edit(ApplicationDbContext db)
    {
        _db = db;
    }
    public Category Category { get; set; }
    
    public void OnGet(int id)
    {
        Category = _db.Category.Find(id);
    }
    
    public async Task<IActionResult> OnPost()
    {
        //Custom Server Validation
        if (Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError(Category.Name, "The Display Order and Name cannot match");
        }
        
        // Custom Server-side validation
        if (!ModelState.IsValid) 
            return Page();
        
        _db.Category.Update(Category);
        await _db.SaveChangesAsync();
        TempData["success"] = "Category updated successfully!";
        return RedirectToPage("Index");
    }
}