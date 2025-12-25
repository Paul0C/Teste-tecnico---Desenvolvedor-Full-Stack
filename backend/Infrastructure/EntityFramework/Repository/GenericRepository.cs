using Backend.Infrastructure.EntityFramework.Repository.Interface;
using Infrastructure.EntityFramework.DataContext;

namespace backend.Infrastructure.EntityFramework.Repository;

public class GenericRepository(DataContext context) : IGenericRepository
{
    private readonly DataContext _context = context;

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }
}