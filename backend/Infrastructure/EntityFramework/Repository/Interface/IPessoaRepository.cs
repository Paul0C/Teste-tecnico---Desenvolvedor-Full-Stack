using backend.Domain.Entities;
using Backend.Infrastructure.EntityFramework.Repository.Interface;

namespace backend.Infrastructure.EntityFramework.Repository.Interface;

public interface IPessoaRepository : IGenericRepository
{
    Task<Pessoa> GetPessoaById(Guid id);
    Task<List<Pessoa>> GetPessoas();
}