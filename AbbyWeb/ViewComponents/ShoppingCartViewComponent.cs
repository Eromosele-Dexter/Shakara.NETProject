using System.Security.Claims;
using Abby.DataAccess.Repository.IRepository;
using Abby.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.ViewComponents;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork;
    public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        int count = 0;
        if (claim != null)
        {
            //user is logged in
            if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
            {
                return View((int)HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else
            {
                count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.SessionCart, count);
                return View(count);
            }
        }
        else
        {
            HttpContext.Session.Clear();
            //user has not logged in
            return View(count);
        }
    }
}