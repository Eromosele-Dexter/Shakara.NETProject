using Abby.DataAccess.Repository;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Customer.Home;

public class Index : PageModel
{
    public readonly IUnitOfWork _unitOfWork;

    public IEnumerable<MenuItem> MenuItemList { get; set; }
    public IEnumerable<Category> CategoryList { get; set; }

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public void OnGet()
    {
        MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType");
        CategoryList = _unitOfWork.Category.GetAll(orderby: u=>u.OrderBy(c=>c.DisplayOrder));
    }
}