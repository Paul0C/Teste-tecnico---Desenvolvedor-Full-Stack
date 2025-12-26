using backend.Domain.Entities;
using backend.Infrastructure.EntityFramework.Repository.Interface;
using Infrastructure.EntityFramework.DataContext;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.EntityFramework.Repository;

public class TransacaoRepository(DataContext context) : GenericRepository(context), ITransacaoRepository 
{
    private readonly DataContext _context = context;

    public async Task<Transacao> GetTransacaoById(Guid id)
    {
        return await _context.Transacoes.AsNoTracking()                                  
                                        .Include(t => t.Categoria)
                                        .Include(t => t.Pessoa)
                                        .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Transacao>> GetTransacoes()
    {
        return await _context.Transacoes.AsNoTracking()
                                        .Include(t => t.Categoria)
                                        .Include(t => t.Pessoa)
                                        .ToListAsync();
    }
}