using backend.Infrastructure.EntityFramework.Repository.Interface;
using Infrastructure.EntityFramework.DataContext;

namespace backend.Infrastructure.EntityFramework.Repository;
 
public class UnitOfWork(DataContext context) : IUnitOfWork
{
    private readonly DataContext _context = context;

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }    
}