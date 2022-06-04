using System.Security.Claims;
using Abby.DataAccess.Repository;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Customer.Cart;

[Authorize]
public class Index : PageModel
{
    public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    public double CartTotal { get; set; }

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        CartTotal = 0;
    }
    public void OnGet()
    {
        // Claim user id of logged in user
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        if (claim != null)
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value
            , includeProperties:"MenuItem,MenuItem.FoodType,MenuItem.Category");
        }

        foreach (var cartItem in ShoppingCartList)
        {
            CartTotal += (cartItem.MenuItem.Price * cartItem.Count);
        }
    }
    
    public IActionResult OnPostPlus(int cartId)
    {
        var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
        _unitOfWork.ShoppingCart.IncrementCount(cart,1);
        return RedirectToPage("/Customer/Cart/Index");
    } 
    
    public IActionResult OnPostRemove(int cartId)
    {
        var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
        _unitOfWork.ShoppingCart.Remove(cart);
        _unitOfWork.Save();
        return RedirectToPage("/Customer/Cart/Index");
    } 
    
    public IActionResult OnPostMinus(int cartId)
    {
        var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
        if (cart.Count == 1)
        {
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
        }
        else
        {
            _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
        }
        
        return RedirectToPage("/Customer/Cart/Index");
    }
}