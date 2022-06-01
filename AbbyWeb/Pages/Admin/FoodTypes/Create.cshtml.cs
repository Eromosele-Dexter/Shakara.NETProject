using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes;
[BindProperties]
public class Create : PageModel
{
    public FoodType FoodType { get; set; }
    
    private readonly IUnitOfWork _unitOfWork;

    public Create(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;   
    }
    
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
      
        // Custom Server-side validation
        if (!ModelState.IsValid) 
            return Page();
        
        _unitOfWork.FoodType.Add(FoodType);
        _unitOfWork.Save();
        TempData["success"] = "FoodType created successfully!";
        return RedirectToPage("Index");
    }
}