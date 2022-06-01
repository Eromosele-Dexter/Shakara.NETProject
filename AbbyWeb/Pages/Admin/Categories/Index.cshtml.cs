using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;

public class Index : PageModel
{
    private readonly ApplicationDbContext _db;
    public IEnumerable<Category> Categories { get; set; }

    public Index(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        Categories = _db.Category;
    }
}