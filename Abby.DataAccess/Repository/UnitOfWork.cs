using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;

namespace Abby.DataAccess.Repository;

public class UnitOfWork:IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    
    public ICategoryRepository Category { get; private set; }
    public IFoodTypeRepository FoodType { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        FoodType = new FoodTypeRepository(_db);
    }

    
    
    public void Save()
    {
        _db.SaveChanges();
    }

    public void Dispose()
    {
      _db.Dispose();
    }
}