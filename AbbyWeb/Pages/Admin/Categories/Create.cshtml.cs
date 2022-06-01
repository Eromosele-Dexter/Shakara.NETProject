using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;
[BindProperties]
public class Create : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public Category Category { get; set; }
    
    public Create(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    
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
        
        _unitOfWork.Category.Add(Category);
        _unitOfWork.Save();
        TempData["success"] = "Category created successfully!";
        return RedirectToPage("Index");
    }
}