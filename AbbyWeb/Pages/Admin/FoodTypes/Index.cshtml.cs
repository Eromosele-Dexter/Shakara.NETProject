using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public IEnumerable<FoodType> FoodTypes { get; set; }

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void OnGet()
    {
        FoodTypes = _unitOfWork.FoodType.GetAll();
    }
}