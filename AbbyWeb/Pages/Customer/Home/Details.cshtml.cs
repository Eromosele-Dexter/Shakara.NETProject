using System.ComponentModel.DataAnnotations;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Customer.Home;

public class Details : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public Details(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public MenuItem MenuItem { get; set; }

    [Range(1, 100, ErrorMessage = "Please select a count between 1 and 100")]
    public int Count { get; set; }

    public void OnGet(int id)
    {
        MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType");
    }
}
    
