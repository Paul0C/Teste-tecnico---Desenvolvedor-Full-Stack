namespace backend.Infrastructure.EntityFramework.Repository.Interface;
 
public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
     
}