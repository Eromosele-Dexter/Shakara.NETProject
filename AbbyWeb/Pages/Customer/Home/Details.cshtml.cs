using System.ComponentModel.DataAnnotations;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Customer.Home;

[Authorize]
public class Details : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public Details(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [BindProperty]
    public ShoppingCart ShoppingCart { get; set; }
    
    public void OnGet(int id)
    {
        ShoppingCart = new()
        {
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType")
        };
    }
}
    
