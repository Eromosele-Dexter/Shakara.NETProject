using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

  
    public OrderController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        var orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties:"ApplicationUser");
        return Json(new { data = orderHeaderList });
    }
    
    
}