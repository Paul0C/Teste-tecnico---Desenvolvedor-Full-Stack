namespace backend.Infrastructure.EntityFramework.Repository.Interface;
 
public interface IUnitOfWork
{
    IPessoaRepository PessoaRepository { get; }
    Task<bool> SaveChangesAsync();
     
}