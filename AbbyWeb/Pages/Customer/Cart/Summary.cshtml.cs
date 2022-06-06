using System.Security.Claims;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace AbbyWeb.Pages.Customer.Cart;

[Authorize]
[BindProperties]
public class Summary : PageModel
{
    public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
    public OrderHeader OrderHeader { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    
    public Summary(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        OrderHeader = new OrderHeader();
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
            OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
        }

        ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
        OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
        OrderHeader.PhoneNumber = applicationUser.PhoneNumber;

    }

    public IActionResult OnPost()
    {
        // Claim user id of logged in user
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        if (claim != null)
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value
                , includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
        

        foreach (var cartItem in ShoppingCartList)
        {
            OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
        }

        OrderHeader.Status = SD.StatusPending;
        OrderHeader.OrderDate = System.DateTime.Now;
        OrderHeader.UserId = claim.Value;
        OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString()
                                                    + " " + OrderHeader.PickUpTime.ToShortTimeString());
        _unitOfWork.OrderHeader.Add(OrderHeader);
        _unitOfWork.Save();

        foreach (var item in ShoppingCartList)
        {
            OrderDetails orderDetails = new()
            {
                MenuItemId = item.MenuItemId,
                OrderId = OrderHeader.Id,
                Name = item.MenuItem.Name,
                Price = item.MenuItem.Price,
                Count = item.Count
            };
            _unitOfWork.OrderDetail.Add(orderDetails);
        }

        
       // _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
        _unitOfWork.Save();

        var domain = "http://localhost:7060/";
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>() 
            ,
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            Mode = "payment",
            SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
            CancelUrl = domain + "Customer/Cart/index",
        };
        
        // add line items
        foreach (var item in ShoppingCartList)
        {
            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.MenuItem.Price * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.MenuItem.Name
                    },
                },
                Quantity = item.Count
            };
            options.LineItems.Add(sessionLineItem);
        }
       
            
        var service = new SessionService();
        Session session = service.Create(options);
        Response.Headers.Add("Location", session.Url);

        OrderHeader.SessionId = session.Id;
        OrderHeader.PaymentIntentId = session.PaymentIntentId;
        _unitOfWork.Save();
        return new StatusCodeResult(303);
        }
        return Page();
    }

    }


