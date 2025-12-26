using backend.Domain.Entities;
using Backend.Infrastructure.EntityFramework.Repository.Interface;

namespace backend.Infrastructure.EntityFramework.Repository.Interface;

public interface ITransacaoRepository : IGenericRepository
{
    Task<Transacao> GetTransacaoById(Guid id);
    Task<List<Transacao>> GetTransacoes();
}