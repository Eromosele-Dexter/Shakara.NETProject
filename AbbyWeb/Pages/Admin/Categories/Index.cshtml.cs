using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public IEnumerable<Category> Categories { get; set; }

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void OnGet()
    {
        Categories = _unitOfWork.Category.GetAll();
    }
}