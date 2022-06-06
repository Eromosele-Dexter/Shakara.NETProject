using Abby.DataAccess.Repository.IRepository;
using Abby.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order;

public class OrderDetails : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
        
    public OrderDetailVM OrderDetailVM { get; set; }
    public OrderDetails(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void OnGet(int id)
    {
        OrderDetailVM = new()
        {
            OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser"),
            OrderDetails = _unitOfWork.OrderDetail.GetAll(u => u.OrderId == id).ToList()
        };
    }
}