using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes;

public class Index : PageModel
{
    private readonly ApplicationDbContext _db;
    public IEnumerable<FoodType> FoodTypes { get; set; }

    public Index(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        FoodTypes = _db.FoodType;
    }
}