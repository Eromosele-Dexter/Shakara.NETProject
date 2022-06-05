using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;

namespace Abby.DataAccess.Repository;

public class OrderDetailsRepository :Repository<OrderDetails>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _db;
    public OrderDetailsRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Save()
    {
        _db.SaveChanges();
    }
    
    public void Update(OrderDetails obj)
    {
        _db.OrderDetails.Update(obj);
    }

    

   

 
   
}