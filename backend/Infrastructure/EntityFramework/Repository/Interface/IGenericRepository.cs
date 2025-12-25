namespace Backend.Infrastructure.EntityFramework.Repository.Interface;

public interface IGenericRepository
{
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
}