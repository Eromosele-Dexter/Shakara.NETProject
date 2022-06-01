using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes;

[BindProperties]
public class Edit : PageModel
{
    public FoodType FoodType { get; set; }
    
    private readonly IUnitOfWork _unitOfWork;

    public Edit(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;   
    }
    
    public void OnGet(int id)
    {
        FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u=>u.Id==id);
    }
    
    public async Task<IActionResult> OnPost()
    {
      
        // Server-side validation
        if (!ModelState.IsValid) 
            return Page();
        
        _unitOfWork.FoodType.Update(FoodType);
        _unitOfWork.Save();
        TempData["success"] = "FoodType updated successfully!";
        return RedirectToPage("Index");
    }
}