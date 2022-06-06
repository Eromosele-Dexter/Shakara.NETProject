using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order;

[Authorize]
public class OrderList : PageModel
{
    public void OnGet()
    {
        
    }
}